using System.ComponentModel;

namespace AgendaFestas.Modelos;

/// <summary>
/// Uma opção de um serviço com sua tabela de preços por dia da semana.
/// Ex.: Decoração "Luxo - Grande (até 150 pessoas)": seg a qui R$ 900, sex R$ 1.100, sáb e dom R$ 1.400.
/// </summary>
public class VariacaoServico
{
    private string _nome = "";

    [DisplayName("Variação / Tamanho")]
    public string Nome
    {
        get => _nome;
        set => _nome = value?.Trim() ?? ""; // a grade envia null quando a célula é apagada
    }

    [DisplayName("Segunda")] public decimal PrecoSegunda { get; set; }
    [DisplayName("Terça")] public decimal PrecoTerca { get; set; }
    [DisplayName("Quarta")] public decimal PrecoQuarta { get; set; }
    [DisplayName("Quinta")] public decimal PrecoQuinta { get; set; }
    [DisplayName("Sexta")] public decimal PrecoSexta { get; set; }
    [DisplayName("Sábado")] public decimal PrecoSabado { get; set; }
    [DisplayName("Domingo")] public decimal PrecoDomingo { get; set; }

    private static readonly DayOfWeek[] SemanaOrdenada =
    {
        DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday,
        DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday
    };

    public decimal ObterPreco(DayOfWeek dia) => dia switch
    {
        DayOfWeek.Monday => PrecoSegunda,
        DayOfWeek.Tuesday => PrecoTerca,
        DayOfWeek.Wednesday => PrecoQuarta,
        DayOfWeek.Thursday => PrecoQuinta,
        DayOfWeek.Friday => PrecoSexta,
        DayOfWeek.Saturday => PrecoSabado,
        _ => PrecoDomingo
    };

    /// <summary>Preços na ordem segunda → domingo.</summary>
    public IEnumerable<decimal> Precos() => SemanaOrdenada.Select(ObterPreco);

    /// <summary>
    /// Junta dias seguidos com o mesmo preço, para o catálogo ficar legível.
    /// Ex.: ("Segunda a Quinta", 800), ("Sexta", 1000), ("Sábado e Domingo", 1500).
    /// </summary>
    public List<(string Dias, decimal Preco)> AgruparPrecosPorDia()
    {
        var grupos = new List<(string, decimal)>();
        int inicio = 0;
        for (int i = 1; i <= SemanaOrdenada.Length; i++)
        {
            bool fimDoGrupo = i == SemanaOrdenada.Length
                || ObterPreco(SemanaOrdenada[i]) != ObterPreco(SemanaOrdenada[inicio]);
            if (!fimDoGrupo)
                continue;

            grupos.Add((DescreverIntervalo(inicio, i - 1), ObterPreco(SemanaOrdenada[inicio])));
            inicio = i;
        }
        return grupos;
    }

    private static string DescreverIntervalo(int inicio, int fim)
    {
        if (inicio == 0 && fim == SemanaOrdenada.Length - 1)
            return "Todos os dias";

        string primeiro = NomeDia(SemanaOrdenada[inicio]);
        string ultimo = NomeDia(SemanaOrdenada[fim]);
        return (fim - inicio) switch
        {
            0 => primeiro,
            1 => $"{primeiro} e {ultimo}",
            _ => $"{primeiro} a {ultimo}"
        };
    }

    public static string NomeDia(DayOfWeek dia) => dia switch
    {
        DayOfWeek.Monday => "Segunda",
        DayOfWeek.Tuesday => "Terça",
        DayOfWeek.Wednesday => "Quarta",
        DayOfWeek.Thursday => "Quinta",
        DayOfWeek.Friday => "Sexta",
        DayOfWeek.Saturday => "Sábado",
        _ => "Domingo"
    };

    public override string ToString() => Nome;
}
