using System.ComponentModel;

namespace AgendaFestas.Modelos;

/// <summary>
/// Serviço oferecido pelo salão (ex.: Aluguel do espaço, Decoração, Garçom).
/// Cada serviço pode ter variações (ex.: Decoração Simples - Pequena) com preço por dia da semana.
/// </summary>
public class TipoServico : Entidade
{
    public string Nome { get; set; } = "";

    [DisplayName("Descrição")]
    public string Descricao { get; set; } = "";

    [Browsable(false)]
    public List<VariacaoServico> Variacoes { get; set; } = new();

    public VariacaoServico? BuscarVariacao(string nome) =>
        Variacoes.FirstOrDefault(v => v.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));

    public override IEnumerable<string> Validar()
    {
        if (string.IsNullOrWhiteSpace(Nome))
            yield return "Informe o nome do serviço.";

        if (Variacoes.Any(v => string.IsNullOrWhiteSpace(v.Nome)))
            yield return "Todas as variações precisam ter um nome.";

        if (Variacoes.Any(v => v.Precos().Any(p => p < 0)))
            yield return "Os preços não podem ser negativos.";

        var repetidas = Variacoes.GroupBy(v => v.Nome.Trim(), StringComparer.OrdinalIgnoreCase)
                                 .Where(g => g.Count() > 1)
                                 .Select(g => g.Key);
        foreach (var nome in repetidas)
            yield return $"A variação \"{nome}\" está repetida.";
    }

    public override string ToString() => Nome;
}
