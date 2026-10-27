using AgendaFestas.Dados;
using AgendaFestas.Servicos;

namespace AgendaFestas.Telas;

public class FormAgendamentos : FormBase
{
    private readonly BancoDados _banco;
    private readonly RelatorioServico _relatorio;
    private readonly AgendamentoServico _servico;
    private readonly DataGridView _grade = Ui.CriarGrade();
    private readonly FiltroPeriodo _periodo = new("De:", DateTime.Today.AddMonths(-1), DateTime.Today.AddYears(1));
    private readonly TextBox _busca = new() { PlaceholderText = "Nome do cliente...", Width = 220 };

    public FormAgendamentos(BancoDados banco)
    {
        _banco = banco;
        _relatorio = new RelatorioServico(banco);
        _servico = new AgendamentoServico(banco);
        Text = "Agendamentos";
        Size = new Size(1200, 650);

        var filtros = new FlowLayoutPanel { AutoSize = true, WrapContents = false, Padding = new Padding(0, 0, 0, 8) };
        filtros.Controls.AddRange([
            _periodo,
            Ui.RotuloSimples("Cliente:"), _busca,
            Ui.Botao("Filtrar", (_, _) => Carregar())
        ]);
        _busca.TextChanged += (_, _) => Carregar();

        var botoes = Ui.BarraBotoes(
            Ui.Botao("Novo agendamento", (_, _) => Abrir(null), destaque: true),
            Ui.Botao("Editar", (_, _) => Editar()),
            Ui.Botao("Excluir", (_, _) => Excluir()));

        Ui.Montar(this, _grade, topo: [Ui.Titulo("Agendamentos"), filtros], rodape: [botoes]);

        _grade.CellDoubleClick += (_, e) =>
        {
            if (e.RowIndex >= 0)
                Editar();
        };
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        Carregar();
    }

    private void Carregar()
    {
        _grade.DataSource = _relatorio.ListarAgendamentos(_periodo.De, _periodo.Ate)
            .Where(l => l.Cliente.Contains(_busca.Text.Trim(), StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    private void Editar()
    {
        if (Ui.Selecionado<LinhaAgendamento>(_grade) is { } linha)
            Abrir(linha.Id);
        else
            Ui.Aviso("Selecione um agendamento na lista.");
    }

    private void Abrir(int? id)
    {
        using var tela = new FormAgendamentoEdicao(_banco, id);
        if (tela.ShowDialog(this) == DialogResult.OK)
            Carregar();
    }

    private void Excluir()
    {
        if (Ui.Selecionado<LinhaAgendamento>(_grade) is not { } linha)
        {
            Ui.Aviso("Selecione um agendamento na lista.");
            return;
        }
        if (!Ui.Confirmar($"Excluir o agendamento de {linha.Cliente} em {linha.Data:dd/MM/yyyy}?"))
            return;

        _servico.Excluir(linha.Id);
        Carregar();
    }
}
