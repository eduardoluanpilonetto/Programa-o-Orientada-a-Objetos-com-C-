using AgendaFestas.Modelos;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace AgendaFestas.Pdf;

/// <summary>Catálogo de serviços e preços para enviar aos clientes.</summary>
public class CatalogoPdf : DocumentoPdf
{
    private readonly List<TipoServico> _servicos;
    private readonly string _observacao;

    public CatalogoPdf(Configuracao configuracao, IEnumerable<TipoServico> servicos) : base(configuracao)
    {
        _servicos = servicos.OrderBy(s => s.Nome).ToList();
        _observacao = configuracao.ObservacaoCatalogo;
    }

    protected override string Titulo => "Catálogo de Serviços e Preços";
    protected override string Subtitulo => "Os serviços podem ser contratados separadamente ou combinados.";

    protected override void ComporConteudo(IContainer container)
    {
        container.Column(coluna =>
        {
            coluna.Spacing(14);

            foreach (var servico in _servicos)
                coluna.Item().Element(c => ComporServico(c, servico));

            if (!string.IsNullOrWhiteSpace(_observacao))
                coluna.Item().PaddingTop(10).Text(_observacao).Italic().FontSize(9).FontColor(Colors.Grey.Darken2);
        });
    }

    private static void ComporServico(IContainer container, TipoServico servico)
    {
        container.Border(0.5f).BorderColor(Colors.Grey.Lighten1).Padding(10).Column(coluna =>
        {
            coluna.Item().Text(servico.Nome).FontSize(13).Bold().FontColor(CorPrincipal);
            if (!string.IsNullOrWhiteSpace(servico.Descricao))
                coluna.Item().PaddingBottom(6).Text(servico.Descricao).FontColor(Colors.Grey.Darken2);

            if (servico.Variacoes.Count == 0)
            {
                coluna.Item().Text("Valor sob consulta.").Italic();
                return;
            }

            coluna.Item().Table(tabela =>
            {
                tabela.ColumnsDefinition(c =>
                {
                    c.RelativeColumn(3);
                    c.RelativeColumn(2);
                    c.RelativeColumn(1.3f);
                });

                tabela.Header(h =>
                {
                    h.Cell().Element(CelulaCabecalho).Text("Opção");
                    h.Cell().Element(CelulaCabecalho).Text("Dias");
                    h.Cell().Element(CelulaCabecalho).AlignRight().Text("Preço");
                });

                foreach (var variacao in servico.Variacoes)
                {
                    var grupos = variacao.AgruparPrecosPorDia();
                    tabela.Cell().RowSpan((uint)grupos.Count).Element(Celula).Text(variacao.Nome).SemiBold();
                    foreach (var (dias, preco) in grupos)
                    {
                        tabela.Cell().Element(Celula).Text(dias);
                        tabela.Cell().Element(Celula).AlignRight().Text(Moeda(preco));
                    }
                }
            });
        });
    }
}
