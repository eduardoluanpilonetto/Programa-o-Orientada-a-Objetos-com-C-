using System.Text.Json;

namespace AgendaFestas.Dados;

/// <summary>Leitura e gravação de arquivos JSON.</summary>
public static class ArquivoJson
{
    private static readonly JsonSerializerOptions Opcoes = new() { WriteIndented = true };

    public static T? Ler<T>(string caminho)
    {
        if (!File.Exists(caminho))
            return default;

        string conteudo = File.ReadAllText(caminho);
        return string.IsNullOrWhiteSpace(conteudo) ? default : JsonSerializer.Deserialize<T>(conteudo, Opcoes);
    }

    /// <summary>
    /// Grava primeiro em um arquivo temporário e depois troca pelo original,
    /// para não corromper os dados se o computador desligar no meio da gravação.
    /// </summary>
    public static void Gravar<T>(string caminho, T dados)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(caminho)!);
        string temporario = caminho + ".tmp";
        File.WriteAllText(temporario, JsonSerializer.Serialize(dados, Opcoes));
        File.Move(temporario, caminho, overwrite: true);
    }
}
