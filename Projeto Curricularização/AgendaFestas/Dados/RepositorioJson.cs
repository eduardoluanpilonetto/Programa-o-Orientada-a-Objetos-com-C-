using System.Text.Json;
using AgendaFestas.Modelos;

namespace AgendaFestas.Dados;

/// <summary>
/// "Banco de dados" em arquivo: cada tipo de entidade fica em um arquivo .json.
/// Os métodos de leitura devolvem cópias, então as telas podem alterar os objetos
/// à vontade e só o que passar por Adicionar/Atualizar é gravado.
/// </summary>
public class RepositorioJson<T> : IRepositorio<T> where T : Entidade
{
    private static readonly JsonSerializerOptions OpcoesJson = new() { WriteIndented = true };

    private readonly string _caminhoArquivo;
    private readonly List<T> _itens;

    public RepositorioJson(string caminhoArquivo)
    {
        _caminhoArquivo = caminhoArquivo;
        _itens = ArquivoJson.Ler<List<T>>(caminhoArquivo) ?? new List<T>();
    }

    public List<T> ObterTodos() => _itens.Select(Copiar).ToList();

    public T? ObterPorId(int id)
    {
        var item = _itens.FirstOrDefault(i => i.Id == id);
        return item is null ? null : Copiar(item);
    }

    public void Adicionar(T entidade)
    {
        entidade.GarantirValido();
        entidade.Id = _itens.Count == 0 ? 1 : _itens.Max(i => i.Id) + 1;
        _itens.Add(Copiar(entidade));
        Salvar();
    }

    public void Atualizar(T entidade)
    {
        entidade.GarantirValido();
        int indice = _itens.FindIndex(i => i.Id == entidade.Id);
        if (indice < 0)
            throw new ValidacaoException("Registro não encontrado. Ele pode ter sido excluído.");

        _itens[indice] = Copiar(entidade);
        Salvar();
    }

    public void Remover(int id)
    {
        _itens.RemoveAll(i => i.Id == id);
        Salvar();
    }

    private void Salvar() => ArquivoJson.Gravar(_caminhoArquivo, _itens);

    private static T Copiar(T item) =>
        JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(item, OpcoesJson), OpcoesJson)!;
}
