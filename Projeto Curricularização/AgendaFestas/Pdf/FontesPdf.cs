namespace AgendaFestas.Pdf;

/// <summary>
/// O QuestPDF lê as fontes do arquivo "QuestPDF.Fonts.Lato.br" na pasta do programa.
/// No .exe único esse arquivo fica embutido, então ele é gravado na pasta ao abrir o sistema.
/// </summary>
public static class FontesPdf
{
    private const string NomeArquivo = "QuestPDF.Fonts.Lato.br";

    public static void Preparar()
    {
        using var recurso = typeof(FontesPdf).Assembly.GetManifestResourceStream(NomeArquivo);
        if (recurso is null)
            return;

        string destino = Path.Combine(AppContext.BaseDirectory, NomeArquivo);
        if (File.Exists(destino) && new FileInfo(destino).Length == recurso.Length)
            return;

        try
        {
            using var arquivo = File.Create(destino);
            recurso.CopyTo(arquivo);
        }
        catch (Exception erro) when (erro is IOException or UnauthorizedAccessException)
        {
            // Sem permissão de gravação na pasta: o sistema abre normalmente e o erro aparece só ao gerar PDF.
        }
    }
}
