using System.Globalization;
using AgendaFestas.Dados;
using AgendaFestas.Modelos;
using AgendaFestas.Telas;
using QuestPDF.Infrastructure;

namespace AgendaFestas;

static class Program
{
    [STAThread]
    static void Main()
    {
        // Datas e valores sempre no padrão brasileiro (dd/MM/yyyy, R$ 1.234,56)
        var culturaBrasil = new CultureInfo("pt-BR");
        CultureInfo.DefaultThreadCurrentCulture = culturaBrasil;
        CultureInfo.DefaultThreadCurrentUICulture = culturaBrasil;
        CultureInfo.CurrentCulture = culturaBrasil;
        CultureInfo.CurrentUICulture = culturaBrasil;

        // Licença gratuita do QuestPDF (empresas com faturamento anual até US$ 1 milhão)
        QuestPDF.Settings.License = LicenseType.Community;

        // Erros de validação viram aviso amigável; outros erros mostram a mensagem sem fechar o programa
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (_, e) => TratarErro(e.Exception);

        ApplicationConfiguration.Initialize();

        // Os dados ficam na pasta "dados" ao lado do .exe
        var banco = new BancoDados(Path.Combine(AppContext.BaseDirectory, "dados"));
        DadosIniciais.CriarSeNecessario(banco);

        Application.Run(new FormPrincipal(banco));
    }

    private static void TratarErro(Exception erro)
    {
        if (erro is ValidacaoException)
            Ui.Aviso(erro.Message);
        else
            MessageBox.Show($"Ocorreu um erro inesperado:\n\n{erro.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
