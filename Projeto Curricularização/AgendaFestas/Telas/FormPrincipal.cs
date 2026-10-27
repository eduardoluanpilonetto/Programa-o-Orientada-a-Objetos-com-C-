using AgendaFestas.Dados;
using AgendaFestas.Pdf;
using AgendaFestas.Servicos;

namespace AgendaFestas.Telas;

public class FormPrincipal : FormBase
{
    private readonly BancoDados _banco;
    private readonly RelatorioServico _relatorio;
    private readonly DataGridView _proximos = Ui.CriarGrade();
    private readonly Label _titulo = Ui.Titulo("");

    public FormPrincipal(BancoDados banco)
    {
        _banco = banco;
        _relatorio = new RelatorioServico(banco);
        Text = "Agenda de Festas";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(1150, 680);
        _titulo.Height = 50;
        _titulo.Font = new Font("Segoe UI", 18f, FontStyle.Bold);

        var menu = new FlowLayoutPanel { AutoSize = true, Padding = new Padding(0, 0, 0, 12) };
        menu.Controls.AddRange([
            BotaoMenu("📅  Novo agendamento", (_, _) => NovoAgendamento(), destaque: true),
            BotaoMenu("📋  Agendamentos", (_, _) => Abrir(new FormAgendamentos(_banco))),
            BotaoMenu("👤  Clientes", (_, _) => Abrir(new FormClientes(_banco))),
            BotaoMenu("🎈  Tipos de serviço", (_, _) => Abrir(new FormTiposServico(_banco))),
            BotaoMenu("📊  Relatórios", (_, _) => Abrir(new FormRelatorios(_banco))),
            BotaoMenu("📄  Catálogo em PDF", (_, _) => GerarCatalogo()),
            BotaoMenu("⚙  Dados da empresa", (_, _) => Abrir(new FormConfiguracoes(_banco)))
        ]);

        var subtitulo = new Label
        {
            Text = "Próximos eventos (duplo clique para abrir)",
            AutoSize = false,
            Height = 30,
            Font = new Font("Segoe UI", 11f, FontStyle.Bold)
        };

        Ui.Montar(this, _proximos, topo: [_titulo, menu, subtitulo]);

        _proximos.CellDoubleClick += (_, e) =>
        {
            if (e.RowIndex >= 0 && Ui.Selecionado<LinhaAgendamento>(_proximos) is { } linha)
                Abrir(new FormAgendamentoEdicao(_banco, linha.Id));
        };
    }

    private static Button BotaoMenu(string texto, EventHandler aoClicar, bool destaque = false)
    {
        var botao = Ui.Botao(texto, aoClicar, destaque);
        botao.AutoSize = false;
        botao.Size = new Size(260, 56);
        botao.Margin = new Padding(0, 0, 10, 10);
        botao.Font = new Font("Segoe UI", 11f);
        return botao;
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        Atualizar();
    }

    private void Atualizar()
    {
        _titulo.Text = _banco.ObterConfiguracao().NomeEmpresa;
        _proximos.DataSource = _relatorio.ListarAgendamentos(DateTime.Today, DateTime.Today.AddYears(5));
    }

    private void Abrir(Form tela)
    {
        using (tela)
            tela.ShowDialog(this);
        Atualizar();
    }

    private void NovoAgendamento()
    {
        if (_banco.TiposServico.ObterTodos().Count == 0)
        {
            Ui.Aviso("Cadastre pelo menos um tipo de serviço antes de agendar.");
            return;
        }
        Abrir(new FormAgendamentoEdicao(_banco));
    }

    private void GerarCatalogo()
    {
        var servicos = _banco.TiposServico.ObterTodos();
        if (servicos.Count == 0)
        {
            Ui.Aviso("Não há serviços cadastrados para montar o catálogo.");
            return;
        }
        Ui.SalvarEAbrirPdf("Catálogo de serviços.pdf",
            caminho => new CatalogoPdf(_banco.ObterConfiguracao(), servicos).Gerar(caminho));
    }
}
