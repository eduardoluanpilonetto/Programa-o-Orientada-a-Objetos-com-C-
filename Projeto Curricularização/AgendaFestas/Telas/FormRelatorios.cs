using AgendaFestas.Dados;
using AgendaFestas.Modelos;
using AgendaFestas.Pdf;
using AgendaFestas.Servicos;

namespace AgendaFestas.Telas;

public class FormRelatorios : FormBase
{
    private readonly BancoDados _banco;
    private readonly RelatorioServico _relatorio;

    // Relatório de agendamentos
    private readonly DataGridView _gradeAgendamentos = Ui.CriarGrade();
    private readonly FiltroPeriodo _periodoAg = new("De:", PrimeiroDiaDoMes(), UltimoDiaDoMes());
    private readonly ComboBox _clienteAg = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 260 };
    private readonly Label _totaisAg = new() { AutoSize = true, Font = new Font("Segoe UI", 10.5f, FontStyle.Bold) };

    // Caixa por cliente
    private readonly DataGridView _gradeCaixa = Ui.CriarGrade();
    private readonly FiltroPeriodo _periodoCx = new("Eventos de:", PrimeiroDiaDoMes(), UltimoDiaDoMes());
    private readonly Label _totaisCx = new() { AutoSize = true, Font = new Font("Segoe UI", 10.5f, FontStyle.Bold) };

    public FormRelatorios(BancoDados banco, int abaInicial = 0)
    {
        _banco = banco;
        _relatorio = new RelatorioServico(banco);
        Text = "Relatórios";
        Size = new Size(1200, 680);

        var abas = new TabControl { Padding = new Point(14, 6) };
        abas.TabPages.Add(CriarAbaAgendamentos());
        abas.TabPages.Add(CriarAbaCaixa());
        abas.SelectedIndex = abaInicial;

        Ui.Montar(this, abas, topo: [Ui.Titulo("Relatórios")]);

        var clientes = new List<Cliente> { new() { Id = 0, Nome = "(Todos os clientes)" } };
        clientes.AddRange(banco.Clientes.ObterTodos().OrderBy(c => c.Nome));
        _clienteAg.DataSource = clientes;
    }

    private static DateTime PrimeiroDiaDoMes() => new(DateTime.Today.Year, DateTime.Today.Month, 1);
    private static DateTime UltimoDiaDoMes() => PrimeiroDiaDoMes().AddMonths(1).AddDays(-1);

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        CarregarAgendamentos();
        CarregarCaixa();
    }

    // ---------- Agendamentos ----------

    private TabPage CriarAbaAgendamentos()
    {
        var filtros = new FlowLayoutPanel { AutoSize = true, WrapContents = false, Padding = new Padding(0, 0, 0, 8) };
        filtros.Controls.AddRange([
            _periodoAg,
            Ui.RotuloSimples("Cliente:"), _clienteAg,
            Ui.Botao("Atualizar", (_, _) => CarregarAgendamentos()),
            Ui.Botao("Gerar PDF", (_, _) => PdfAgendamentos(), destaque: true)
        ]);

        var aba = new TabPage("Agendamentos") { Padding = new Padding(10), BackColor = Color.White };
        Ui.Montar(aba, _gradeAgendamentos, topo: [filtros], rodape: [Ui.BarraBotoes(_totaisAg)]);
        return aba;
    }

    private int? ClienteFiltro => _clienteAg.SelectedItem is Cliente { Id: > 0 } c ? c.Id : null;

    private List<LinhaAgendamento> ConsultarAgendamentos() =>
        _relatorio.ListarAgendamentos(_periodoAg.De, _periodoAg.Ate, ClienteFiltro);

    private void CarregarAgendamentos()
    {
        var linhas = ConsultarAgendamentos();
        _gradeAgendamentos.DataSource = linhas;
        _totaisAg.Text = $"{linhas.Count} agendamento(s)   •   Total: {linhas.Sum(l => l.ValorTotal):C2}   •   " +
                         $"Pago: {linhas.Sum(l => l.ValorPago):C2}   •   A receber: {linhas.Sum(l => l.Saldo):C2}";
    }

    private void PdfAgendamentos()
    {
        var linhas = ConsultarAgendamentos();
        Ui.SalvarEAbrirPdf($"Agendamentos {_periodoAg.De:dd-MM-yyyy} a {_periodoAg.Ate:dd-MM-yyyy}.pdf",
            caminho => new RelatorioAgendamentosPdf(_banco.ObterConfiguracao(), linhas, _periodoAg.De, _periodoAg.Ate).Gerar(caminho));
    }

    // ---------- Caixa por cliente ----------

    private TabPage CriarAbaCaixa()
    {
        var filtros = new FlowLayoutPanel { AutoSize = true, WrapContents = false, Padding = new Padding(0, 0, 0, 8) };
        filtros.Controls.AddRange([
            _periodoCx,
            Ui.Botao("Atualizar", (_, _) => CarregarCaixa()),
            Ui.Botao("Gerar PDF", (_, _) => PdfCaixa(), destaque: true)
        ]);

        var aba = new TabPage("Caixa por cliente") { Padding = new Padding(10), BackColor = Color.White };
        Ui.Montar(aba, _gradeCaixa, topo: [filtros], rodape: [Ui.BarraBotoes(_totaisCx)]);
        return aba;
    }

    private void CarregarCaixa()
    {
        var resumos = _relatorio.CaixaPorCliente(_periodoCx.De, _periodoCx.Ate);
        _gradeCaixa.DataSource = resumos;
        _totaisCx.Text = $"Total contratado: {resumos.Sum(r => r.TotalContratado):C2}   •   " +
                         $"Recebido: {resumos.Sum(r => r.TotalPago):C2}   •   A receber: {resumos.Sum(r => r.AReceber):C2}";
    }

    private void PdfCaixa()
    {
        var resumos = _relatorio.CaixaPorCliente(_periodoCx.De, _periodoCx.Ate);
        Ui.SalvarEAbrirPdf($"Caixa por cliente {_periodoCx.De:dd-MM-yyyy} a {_periodoCx.Ate:dd-MM-yyyy}.pdf",
            caminho => new RelatorioCaixaPdf(_banco.ObterConfiguracao(), resumos, _periodoCx.De, _periodoCx.Ate).Gerar(caminho));
    }
}
