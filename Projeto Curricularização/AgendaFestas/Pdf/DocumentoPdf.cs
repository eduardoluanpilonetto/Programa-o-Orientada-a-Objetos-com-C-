using AgendaFestas.Modelos;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace AgendaFestas.Pdf;

/// <summary>
/// Modelo comum a todos os PDFs do sistema: cabeçalho com dados da empresa,
/// título e rodapé com numeração. Cada documento só implementa o seu conteúdo.
/// </summary>
public abstract class DocumentoPdf
{
    protected static readonly string CorPrincipal = Colors.DeepPurple.Darken2;

    private readonly Configuracao _configuracao;

    protected DocumentoPdf(Configuracao configuracao) => _configuracao = configuracao;

    protected abstract string Titulo { get; }
    protected virtual string Subtitulo => "";

    protected abstract void ComporConteudo(IContainer container);

    public void Gerar(string caminho)
    {
        Document.Create(documento =>
        {
            documento.Page(pagina =>
            {
                pagina.Size(PageSizes.A4);
                pagina.Margin(1.5f, Unit.Centimetre);
                pagina.DefaultTextStyle(estilo => estilo.FontSize(10));

                pagina.Header().Element(ComporCabecalho);
                pagina.Content().PaddingVertical(10).Element(ComporConteudo);
                pagina.Footer().AlignCenter().Text(texto =>
                {
                    texto.DefaultTextStyle(e => e.FontSize(8).FontColor(Colors.Grey.Darken1));
                    texto.Span("Página ");
                    texto.CurrentPageNumber();
                    texto.Span(" de ");
                    texto.TotalPages();
                });
            });
        })
        .WithMetadata(new DocumentMetadata { Title = Titulo, Author = _configuracao.NomeEmpresa })
        .GeneratePdf(caminho);
    }

    private void ComporCabecalho(IContainer container)
    {
        container.Column(coluna =>
        {
            coluna.Item().Row(linha =>
            {
                linha.RelativeItem().Column(empresa =>
                {
                    empresa.Item().Text(_configuracao.NomeEmpresa).FontSize(18).Bold().FontColor(CorPrincipal);
                    if (!string.IsNullOrWhiteSpace(_configuracao.Endereco))
                        empresa.Item().Text(_configuracao.Endereco).FontColor(Colors.Grey.Darken2);
                    if (!string.IsNullOrWhiteSpace(_configuracao.Telefone))
                        empresa.Item().Text($"Telefone: {_configuracao.Telefone}").FontColor(Colors.Grey.Darken2);
                });
                linha.ConstantItem(120).AlignRight().Text($"Emitido em {DateTime.Now:dd/MM/yyyy HH:mm}")
                    .FontSize(8).FontColor(Colors.Grey.Darken1);
            });

            coluna.Item().PaddingTop(8).LineHorizontal(1.5f).LineColor(CorPrincipal);
            coluna.Item().PaddingTop(8).Text(Titulo).FontSize(15).Bold();
            if (!string.IsNullOrWhiteSpace(Subtitulo))
                coluna.Item().Text(Subtitulo).FontColor(Colors.Grey.Darken2);
        });
    }

    // Estilos reutilizados nas tabelas dos relatórios
    protected static IContainer CelulaCabecalho(IContainer c) =>
        c.Background(CorPrincipal).PaddingVertical(4).PaddingHorizontal(4).DefaultTextStyle(e => e.FontColor(Colors.White).Bold().FontSize(9));

    protected static IContainer Celula(IContainer c) =>
        c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).PaddingVertical(3).PaddingHorizontal(4).DefaultTextStyle(e => e.FontSize(9));

    protected static IContainer CelulaTotal(IContainer c) =>
        c.Background(Colors.Grey.Lighten3).PaddingVertical(4).PaddingHorizontal(4).DefaultTextStyle(e => e.Bold().FontSize(9));

    protected static string Moeda(decimal valor) => valor.ToString("C2");
}
