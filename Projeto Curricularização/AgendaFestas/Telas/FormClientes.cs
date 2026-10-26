using AgendaFestas.Dados;
using AgendaFestas.Modelos;
using AgendaFestas.Servicos;

namespace AgendaFestas.Telas;

public class FormClientes : FormBase
{
    private readonly ClienteServico _servico;
    private readonly DataGridView _grade = Ui.CriarGrade();
    private readonly TextBox _busca = new() { PlaceholderText = "Buscar por nome, telefone ou CPF..." };
    private readonly TextBox _nome = new() { MaxLength = 100 };
    private readonly TextBox _endereco = new() { MaxLength = 200 };
    private readonly TextBox _telefone = new() { MaxLength = 20 };
    private readonly MaskedTextBox _cpf = new() { Mask = @"000\.000\.000-00" };
    private int _idAtual;

    /// <summary>Id do último cliente salvo (usado pela tela de agendamento para já selecioná-lo).</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
    public int? ClienteSalvoId { get; private set; }

    public FormClientes(BancoDados banco)
    {
        _servico = new ClienteServico(banco);
        Text = "Clientes";
        Size = new Size(1050, 600);

        var formulario = Ui.Formulario();
        Ui.Campo(formulario, "Nome *", _nome);
        Ui.Campo(formulario, "Endereço *", _endereco);
        Ui.Campo(formulario, "Telefone *", _telefone);
        Ui.Campo(formulario, "CPF", _cpf);

        var painelDados = new Panel { Width = 420, Padding = new Padding(16, 0, 0, 0) };
        Ui.Montar(painelDados, new Panel(),
            topo: [Ui.Titulo("Dados do cliente"), formulario],
            rodape: [Ui.BarraBotoes(
                Ui.Botao("Novo", (_, _) => Novo()),
                Ui.Botao("Salvar", (_, _) => Salvar(), destaque: true),
                Ui.Botao("Excluir", (_, _) => Excluir()))]);

        _busca.Dock = DockStyle.Top;
        _busca.TextChanged += (_, _) => Carregar();
        var painelLista = new Panel();
        Ui.Montar(painelLista, _grade, topo: [_busca, new Panel { Height = 8 }]);

        Ui.Montar(this, painelLista, topo: [Ui.Titulo("Clientes")], direita: painelDados);

        _grade.SelectionChanged += (_, _) =>
        {
            if (Ui.Selecionado<Cliente>(_grade) is { } cliente)
                Preencher(cliente);
        };
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        Carregar();
        _nome.Focus();
    }

    private void Carregar(int? selecionarId = null)
    {
        _grade.DataSource = _servico.Listar(_busca.Text);
        _grade.CurrentCell = null;

        foreach (DataGridViewRow linha in _grade.Rows)
        {
            if (linha.DataBoundItem is Cliente c && c.Id == selecionarId)
            {
                _grade.CurrentCell = linha.Cells[0];
                return;
            }
        }
        if (selecionarId is null)
            Limpar();
    }

    private void Preencher(Cliente cliente)
    {
        _idAtual = cliente.Id;
        _nome.Text = cliente.Nome;
        _endereco.Text = cliente.Endereco;
        _telefone.Text = cliente.Telefone;
        _cpf.Text = cliente.Cpf;
    }

    private void Limpar()
    {
        _idAtual = 0;
        _nome.Clear();
        _endereco.Clear();
        _telefone.Clear();
        _cpf.Clear();
    }

    private void Novo()
    {
        _grade.CurrentCell = null;
        Limpar();
        _nome.Focus();
    }

    private void Salvar()
    {
        bool cpfVazio = !_cpf.Text.Any(char.IsDigit);
        var cliente = new Cliente
        {
            Id = _idAtual,
            Nome = _nome.Text.Trim(),
            Endereco = _endereco.Text.Trim(),
            Telefone = _telefone.Text.Trim(),
            Cpf = cpfVazio ? "" : _cpf.Text
        };
        _servico.Salvar(cliente);

        ClienteSalvoId = cliente.Id;
        _busca.Clear();
        Carregar(cliente.Id);
        Ui.Info("Cliente salvo com sucesso.");
    }

    private void Excluir()
    {
        if (_idAtual == 0)
        {
            Ui.Aviso("Selecione um cliente na lista.");
            return;
        }
        if (!Ui.Confirmar($"Excluir o cliente \"{_nome.Text}\"?"))
            return;

        _servico.Excluir(_idAtual);
        Carregar();
    }
}
