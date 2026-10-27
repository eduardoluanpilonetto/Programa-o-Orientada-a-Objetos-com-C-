using AgendaFestas.Modelos;
using AgendaFestas.Servicos;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace AgendaFestas.Pdf;

public class RelatorioAgendamentosPdf : DocumentoPdf
{
    private readonly List<LinhaAgendamento> _linhas;
    private readonly DateTime _de;
    private readonly DateTime _ate;

    public RelatorioAgendamentosPdf(Configuracao configuracao, List<LinhaAgendamento> linhas, DateTime de, DateTime ate)
        : base(configuracao)
    {
        _linhas = linhas;
        _de = de;
        _ate = ate;
    }

    protected override string Titulo => "Relatório de Agendamentos";
    protected override string Subtitulo => $"Período: {_de:dd/MM/yyyy} a {_ate:dd/MM/yyyy}  •  {_linhas.Count} agendamento(s)";

    protected override void ComporConteudo(IContainer container)
    {
        container.Table(tabela =>
        {
            tabela.ColumnsDefinition(c =>
            {
                c.ConstantColumn(58);  // data
                c.RelativeColumn(2);   // cliente
                c.RelativeColumn(3);   // serviços
                c.ConstantColumn(62);  // total
                c.ConstantColumn(62);  // pago
                c.ConstantColumn(62);  // a receber
            });

            tabela.Header(h =>
            {
                h.Cell().Element(CelulaCabecalho).Text("Data");
                h.Cell().Element(CelulaCabecalho).Text("Cliente");
                h.Cell().Element(CelulaCabecalho).Text("Serviços");
                h.Cell().Element(CelulaCabecalho).AlignRight().Text("Total");
                h.Cell().Element(CelulaCabecalho).AlignRight().Text("Pago");
                h.Cell().Element(CelulaCabecalho).AlignRight().Text("A receber");
            });

            foreach (var linha in _linhas)
            {
                tabela.Cell().Element(Celula).Text($"{linha.Data:dd/MM/yyyy}\n{linha.DiaSemana}");
                tabela.Cell().Element(Celula).Text($"{linha.Cliente}\n{linha.Telefone}");
                tabela.Cell().Element(Celula).Text(linha.Servicos);
                tabela.Cell().Element(Celula).AlignRight().Text(Moeda(linha.ValorTotal));
                tabela.Cell().Element(Celula).AlignRight().Text(Moeda(linha.ValorPago));
                tabela.Cell().Element(Celula).AlignRight().Text(Moeda(linha.Saldo));
            }

            tabela.Cell().ColumnSpan(3).Element(CelulaTotal).Text("Totais");
            tabela.Cell().Element(CelulaTotal).AlignRight().Text(Moeda(_linhas.Sum(l => l.ValorTotal)));
            tabela.Cell().Element(CelulaTotal).AlignRight().Text(Moeda(_linhas.Sum(l => l.ValorPago)));
            tabela.Cell().Element(CelulaTotal).AlignRight().Text(Moeda(_linhas.Sum(l => l.Saldo)));
        });
    }
}
