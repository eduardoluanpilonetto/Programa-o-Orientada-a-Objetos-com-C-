using AgendaFestas.Modelos;
using AgendaFestas.Servicos;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace AgendaFestas.Pdf;

/// <summary>Caixa por cliente: totais de cada cliente com o detalhe dos seus agendamentos.</summary>
public class RelatorioCaixaPdf : DocumentoPdf
{
    private readonly List<ResumoCaixaCliente> _resumos;
    private readonly DateTime _de;
    private readonly DateTime _ate;

    public RelatorioCaixaPdf(Configuracao configuracao, List<ResumoCaixaCliente> resumos, DateTime de, DateTime ate)
        : base(configuracao)
    {
        _resumos = resumos;
        _de = de;
        _ate = ate;
    }

    protected override string Titulo => "Relatório de Caixa por Cliente";
    protected override string Subtitulo => $"Período: {_de:dd/MM/yyyy} a {_ate:dd/MM/yyyy}";

    protected override void ComporConteudo(IContainer container)
    {
        container.Column(coluna =>
        {
            coluna.Spacing(12);
            coluna.Item().Element(ComporTotaisGerais);

            foreach (var resumo in _resumos)
                coluna.Item().Element(c => ComporCliente(c, resumo));
        });
    }

    private void ComporTotaisGerais(IContainer container)
    {
        container.Row(linha =>
        {
            linha.Spacing(8);
            Quadro(linha.RelativeItem(), "Total contratado", _resumos.Sum(r => r.TotalContratado));
            Quadro(linha.RelativeItem(), "Total recebido", _resumos.Sum(r => r.TotalPago));
            Quadro(linha.RelativeItem(), "A receber", _resumos.Sum(r => r.AReceber));
        });
    }

    private static void Quadro(IContainer container, string rotulo, decimal valor)
    {
        container.Background(Colors.Grey.Lighten4).Padding(8).Column(c =>
        {
            c.Item().Text(rotulo).FontSize(9).FontColor(Colors.Grey.Darken2);
            c.Item().Text(Moeda(valor)).FontSize(14).Bold().FontColor(CorPrincipal);
        });
    }

    private static void ComporCliente(IContainer container, ResumoCaixaCliente resumo)
    {
        container.Column(coluna =>
        {
            coluna.Item().Text(text =>
            {
                text.Span(resumo.Cliente).FontSize(12).Bold().FontColor(CorPrincipal);
                if (!string.IsNullOrWhiteSpace(resumo.Telefone))
                    text.Span($"   {resumo.Telefone}").FontColor(Colors.Grey.Darken2);
            });

            coluna.Item().PaddingTop(4).Table(tabela =>
            {
                tabela.ColumnsDefinition(c =>
                {
                    c.ConstantColumn(62);
                    c.RelativeColumn();
                    c.ConstantColumn(66);
                    c.ConstantColumn(66);
                    c.ConstantColumn(66);
                });

                tabela.Header(h =>
                {
                    h.Cell().Element(CelulaCabecalho).Text("Data");
                    h.Cell().Element(CelulaCabecalho).Text("Serviços");
                    h.Cell().Element(CelulaCabecalho).AlignRight().Text("Total");
                    h.Cell().Element(CelulaCabecalho).AlignRight().Text("Pago");
                    h.Cell().Element(CelulaCabecalho).AlignRight().Text("A receber");
                });

                foreach (var ag in resumo.Agendamentos)
                {
                    tabela.Cell().Element(Celula).Text($"{ag.Data:dd/MM/yyyy}");
                    tabela.Cell().Element(Celula).Text(ag.Servicos);
                    tabela.Cell().Element(Celula).AlignRight().Text(Moeda(ag.ValorTotal));
                    tabela.Cell().Element(Celula).AlignRight().Text(Moeda(ag.ValorPago));
                    tabela.Cell().Element(Celula).AlignRight().Text(Moeda(ag.Saldo));
                }

                tabela.Cell().ColumnSpan(2).Element(CelulaTotal).Text($"Subtotal ({resumo.Quantidade} agendamento(s))");
                tabela.Cell().Element(CelulaTotal).AlignRight().Text(Moeda(resumo.TotalContratado));
                tabela.Cell().Element(CelulaTotal).AlignRight().Text(Moeda(resumo.TotalPago));
                tabela.Cell().Element(CelulaTotal).AlignRight().Text(Moeda(resumo.AReceber));
            });
        });
    }
}
