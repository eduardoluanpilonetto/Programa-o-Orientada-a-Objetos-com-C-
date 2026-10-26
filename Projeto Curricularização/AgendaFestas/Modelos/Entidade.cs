using System.ComponentModel;

namespace AgendaFestas.Modelos;

/// <summary>
/// Classe base de tudo que é gravado no "banco" (arquivos JSON).
/// Toda entidade tem um Id e sabe validar os próprios dados.
/// </summary>
public abstract class Entidade
{
    [Browsable(false)]
    public int Id { get; set; }

    /// <summary>Retorna a lista de erros encontrados. Lista vazia = dados válidos.</summary>
    public abstract IEnumerable<string> Validar();

    /// <summary>Lança ValidacaoException se houver algum erro.</summary>
    public void GarantirValido()
    {
        var erros = Validar().ToList();
        if (erros.Count > 0)
            throw new ValidacaoException(erros);
    }
}

/// <summary>Erro de regra de negócio, exibido ao usuário como aviso (não como falha do sistema).</summary>
public class ValidacaoException : Exception
{
    public ValidacaoException(string mensagem) : base(mensagem) { }

    public ValidacaoException(IEnumerable<string> erros) : base(string.Join(Environment.NewLine, erros)) { }
}
