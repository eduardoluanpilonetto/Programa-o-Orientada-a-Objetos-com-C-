using AgendaFestas.Modelos;

namespace AgendaFestas.Dados;

/// <summary>
/// Na primeira execução cadastra alguns serviços de exemplo para o sistema não abrir vazio.
/// Os preços são apenas ilustrativos e devem ser ajustados na tela de Serviços.
/// </summary>
public static class DadosIniciais
{
    public static void CriarSeNecessario(BancoDados banco)
    {
        if (banco.TiposServico.ObterTodos().Count > 0)
            return;

        banco.TiposServico.Adicionar(new TipoServico
        {
            Nome = "Aluguel do espaço",
            Descricao = "Uso do salão por até 5 horas, com mesas, cadeiras e cozinha de apoio.",
            Variacoes = { Variacao("Salão completo", 600, 900, 1500) }
        });

        banco.TiposServico.Adicionar(new TipoServico
        {
            Nome = "Decoração",
            Descricao = "Montagem e desmontagem da decoração temática.",
            Variacoes =
            {
                Variacao("Simples - Pequena (até 50 pessoas)", 250, 300, 350),
                Variacao("Simples - Grande (até 120 pessoas)", 400, 450, 500),
                Variacao("Luxo - Pequena (até 50 pessoas)", 600, 700, 800),
                Variacao("Luxo - Grande (até 120 pessoas)", 900, 1000, 1200)
            }
        });

        banco.TiposServico.Adicionar(new TipoServico
        {
            Nome = "Garçom",
            Descricao = "Garçom para atendimento durante a festa (valor por profissional).",
            Variacoes = { Variacao("Por garçom (5 horas)", 150, 150, 180) }
        });
    }

    private static VariacaoServico Variacao(string nome, decimal segAQui, decimal sexta, decimal fimDeSemana) => new()
    {
        Nome = nome,
        PrecoSegunda = segAQui,
        PrecoTerca = segAQui,
        PrecoQuarta = segAQui,
        PrecoQuinta = segAQui,
        PrecoSexta = sexta,
        PrecoSabado = fimDeSemana,
        PrecoDomingo = fimDeSemana
    };
}
