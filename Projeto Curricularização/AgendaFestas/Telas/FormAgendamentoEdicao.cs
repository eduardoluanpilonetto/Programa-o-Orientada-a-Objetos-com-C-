using System.ComponentModel;
using AgendaFestas.Dados;
using AgendaFestas.Modelos;
using AgendaFestas.Servicos;

namespace AgendaFestas.Telas;

public class FormAgendamentoEdicao : FormBase
{
    private readonly BancoDados _banco;
    private readonly AgendamentoServico _servico;
    private readonly Agendamento _agendamento;
    private readonly BindingList<ItemAgendamento> _itens;

    private readonly ComboBox _cliente = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 380 };
    private readonly DateTimePicker _data = Ui.CampoData(DateTime.Today);
    private readonly Label _diaSemana = Ui.RotuloSimples("");
    private readonly ComboBox _tipo = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 220 };
    private readonly ComboBox _variacao = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 300 };
    private readonly NumericUpDown _quantidade = new() { Minimum = 1, Maximum = 1000, Value = 1, Width = 60 };
    private readonly NumericUpDown _valorUnitario = Ui.CampoValor();
    private readonly DataGridView _gradeItens = Ui.CriarGrade(somenteLeitura: false);
    private readonly TextBox _observacoes = new() { Multiline = true, Height = 50, ScrollBars = ScrollBars.Vertical };
    private readonly Label _valorServicos = new() { AutoSize = true, Font = new Font("Segoe UI", 11f, FontStyle.Bold), Margin = new Padding(0, 6, 24, 0) };
    private readonly NumericUpDown _desconto = Ui.CampoValor();
    private readonly Label _valorTotal = new() { AutoSize = true, Font = new Font("Segoe UI", 14f, FontStyle.Bold), ForeColor = Ui.CorPrincipal, Margin = new Padding(0, 0, 24, 0) };
    private readonly NumericUpDown _valorPago = Ui.CampoValor();
    private readonly Label _saldo = new() { AutoSize = true, Font = new Font("Segoe UI", 12f, FontStyle.Bold), Margin = new Padding(20, 4, 0, 0) };
    private DateTime _dataAnterior;

    public FormAgendamentoEdicao(BancoDados banco, int? agendamentoId = null, DateTime? dataSugerida = null)
    {
        _banco = banco;
        _servico = new AgendamentoServico(banco);
        _agendamento = (agendamentoId is null ? null : banco.Agendamentos.ObterPorId(agendamentoId.Value))
                       ?? new Agendamento { DataEvento = dataSugerida ?? DateTime.Today };
        _itens = new BindingList<ItemAgendamento>(_agendamento.Itens);

        Text = _agendamento.Id == 0 ? "Novo agendamento" : "Editar agendamento";
        Size = new Size(1180, 740);
        MinimumSize = new Size(900, 600);

        MontarTela();
        CarregarClientes(_agendamento.ClienteId);
        _tipo.DataSource = banco.TiposServico.ObterTodos().OrderBy(t => t.Nome).ToList();

        // Não permite escolher dia que já passou (festa antiga pode ser aberta para registrar pagamento)
        _dataAnterior = _agendamento.DataEvento;
        _data.Value = _agendamento.DataEvento;
        _data.MinDate = _agendamento.DataEvento < DateTime.Today ? _agendamento.DataEvento : DateTime.Today;
        _desconto.Value = _agendamento.Desconto;
        _valorPago.Value = _agendamento.ValorPago;
        _observacoes.Text = _agendamento.Observacoes;
        _gradeItens.DataSource = _itens;
        AtualizarDiaSemana();
        AtualizarTotais();
    }

    private void MontarTela()
    {
        // --- Cliente e data ---
        var linhaCliente = new FlowLayoutPanel { AutoSize = true, WrapContents = false, Margin = Padding.Empty };
        linhaCliente.Controls.Add(_cliente);
        linhaCliente.Controls.Add(Ui.Botao("+ Novo cliente", (_, _) => CadastrarCliente()));

        var linhaData = new FlowLayoutPanel { AutoSize = true, WrapContents = false, Margin = Padding.Empty };
        linhaData.Controls.Add(_data);
        linhaData.Controls.Add(_diaSemana);
        _data.ValueChanged += (_, _) =>
        {
            AtualizarDiaSemana();
            AtualizarPrecosDaData();
        };

        var formulario = Ui.Formulario();
        Ui.Campo(formulario, "Cliente *", linhaCliente).Dock = DockStyle.None;
        Ui.Campo(formulario, "Data do evento *", linhaData).Dock = DockStyle.None;

        // --- Serviços ---
        var adicionar = new FlowLayoutPanel { AutoSize = true, WrapContents = true, Padding = new Padding(0, 4, 0, 8) };
        adicionar.Controls.AddRange([
            Ui.RotuloSimples("Serviço:"), _tipo,
            Ui.RotuloSimples("Opção:"), _variacao,
            Ui.RotuloSimples("Qtde:"), _quantidade,
            Ui.RotuloSimples("Valor unit.:"), _valorUnitario,
            Ui.Botao("Adicionar", (_, _) => AdicionarItem(), destaque: true)
        ]);
        _tipo.SelectedIndexChanged += (_, _) => AtualizarVariacoes();
        _variacao.SelectedIndexChanged += (_, _) => SugerirValor();

        _gradeItens.DataBindingComplete += (_, _) =>
        {
            foreach (DataGridViewColumn coluna in _gradeItens.Columns)
                coluna.ReadOnly = coluna.DataPropertyName is not (nameof(ItemAgendamento.Quantidade) or nameof(ItemAgendamento.ValorUnitario));
            if (_gradeItens.Columns[nameof(ItemAgendamento.Variacao)] is { } colunaVariacao)
                colunaVariacao.FillWeight = 200;
        };
        _gradeItens.CellValueChanged += (_, _) => AtualizarTotais();
        _itens.ListChanged += (_, _) => AtualizarTotais();

        var dicaGrade = new Label
        {
            Text = "Quantidade e valor unitário podem ser alterados na tabela. Ao mudar a data, os preços de tabela\n" +
                   "são atualizados para o novo dia; valores digitados à mão são mantidos.",
            AutoSize = true,
            ForeColor = Color.DimGray,
            Padding = new Padding(0, 0, 0, 0)
        };
        var botoesItens = Ui.BarraBotoes(
            Ui.Botao("Remover serviço", (_, _) => RemoverItem()),
            dicaGrade);

        var grupoServicos = new GroupBox { Text = "Serviços contratados", Padding = new Padding(10) };
        Ui.Montar(grupoServicos, _gradeItens, topo: [adicionar], rodape: [botoesItens]);

        // --- Totais e pagamento ---
        var formObs = Ui.Formulario();
        Ui.Campo(formObs, "Observações", _observacoes);

        var totais = new FlowLayoutPanel { AutoSize = true, WrapContents = true, Padding = new Padding(0, 6, 0, 0) };
        totais.Controls.AddRange([
            Ui.RotuloSimples("Serviços:"), _valorServicos,
            Ui.RotuloSimples("Desconto:"), _desconto,
            Ui.RotuloSimples("   Total:"), _valorTotal,
            Ui.RotuloSimples("Valor pago:"), _valorPago,
            _saldo
        ]);
        _desconto.Margin = new Padding(0, 3, 24, 0);
        _desconto.ValueChanged += (_, _) => AtualizarTotais();
        _valorPago.ValueChanged += (_, _) => AtualizarTotais();

        var botoes = Ui.BarraBotoes(
            Ui.Botao("Salvar agendamento", (_, _) => Salvar(), destaque: true),
            Ui.Botao("Cancelar", (_, _) => Close()));

        Ui.Montar(this, grupoServicos,
            topo: [Ui.Titulo(Text), formulario],
            rodape: [new Panel { Height = 8 }, formObs, totais, botoes]);
    }

    private void CarregarClientes(int selecionarId)
    {
        var clientes = _banco.Clientes.ObterTodos().OrderBy(c => c.Nome).ToList();
        _cliente.DataSource = clientes;
        _cliente.SelectedItem = clientes.FirstOrDefault(c => c.Id == selecionarId);
        if (_cliente.SelectedItem is null)
            _cliente.SelectedIndex = -1;
    }

    private void CadastrarCliente()
    {
        using var tela = new FormClientes(_banco);
        tela.ShowDialog(this);
        int atual = (_cliente.SelectedItem as Cliente)?.Id ?? 0;
        CarregarClientes(tela.ClienteSalvoId ?? atual);
    }

    private void AtualizarDiaSemana()
    {
        _diaSemana.Text = _data.Value.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday
            ? VariacaoServico.NomeDia(_data.Value.DayOfWeek)
            : VariacaoServico.NomeDia(_data.Value.DayOfWeek) + "-feira";
        SugerirValor();
    }

    private void AtualizarVariacoes()
    {
        var tipo = _tipo.SelectedItem as TipoServico;
        _variacao.DataSource = tipo?.Variacoes.ToList() ?? new List<VariacaoServico>();
        _variacao.Enabled = tipo?.Variacoes.Count > 0;
        SugerirValor();
    }

    /// <summary>Preenche o valor unitário com o preço da tabela para o dia da semana escolhido.</summary>
    private void SugerirValor()
    {
        if (_variacao.SelectedItem is VariacaoServico variacao)
            _valorUnitario.Value = AgendamentoServico.PrecoSugerido(variacao, _data.Value);
        else if (!_variacao.Enabled)
            _valorUnitario.Value = 0;
    }

    private void AdicionarItem()
    {
        if (_tipo.SelectedItem is not TipoServico tipo)
        {
            Ui.Aviso("Selecione um serviço. Se a lista estiver vazia, cadastre os serviços primeiro.");
            return;
        }
        if (_valorUnitario.Value == 0 && !Ui.Confirmar("O valor do serviço está zerado. Adicionar mesmo assim?"))
            return;

        _gradeItens.EndEdit();
        bool substituiu = _agendamento.AdicionarServico(new ItemAgendamento
        {
            TipoServicoId = tipo.Id,
            NomeServico = tipo.Nome,
            Variacao = (_variacao.SelectedItem as VariacaoServico)?.Nome ?? "",
            Quantidade = (int)_quantidade.Value,
            ValorUnitario = _valorUnitario.Value
        });
        _itens.ResetBindings(); // _itens enxerga a mesma lista de _agendamento.Itens
        _quantidade.Value = 1;

        if (substituiu)
            Ui.Info($"\"{tipo.Nome}\" já estava no agendamento e foi substituído pela nova opção.");
    }

    private void RemoverItem()
    {
        if (Ui.Selecionado<ItemAgendamento>(_gradeItens) is { } item)
            _itens.Remove(item);
        else
            Ui.Aviso("Selecione o serviço que deseja remover.");
    }

    private void AtualizarPrecosDaData()
    {
        if (_data.Value.Date == _dataAnterior.Date)
            return;

        _gradeItens.EndEdit();
        _servico.AtualizarPrecosParaNovaData(_itens, _dataAnterior, _data.Value);
        _dataAnterior = _data.Value;
        _itens.ResetBindings();
    }

    private void AtualizarTotais()
    {
        decimal servicos = _itens.Sum(i => i.Subtotal);
        decimal total = servicos - _desconto.Value;
        decimal saldo = total - _valorPago.Value;
        _valorServicos.Text = servicos.ToString("C2");
        _valorTotal.Text = total.ToString("C2");
        _saldo.Text = saldo > 0 ? $"A receber: {saldo:C2}" : saldo == 0 ? "Quitado" : $"Pago a mais: {-saldo:C2}";
        _saldo.ForeColor = saldo > 0 ? Color.Firebrick : Color.SeaGreen;
        _gradeItens.Invalidate(); // redesenha a coluna Subtotal
    }

    private void Salvar()
    {
        _gradeItens.EndEdit();

        _agendamento.ClienteId = (_cliente.SelectedItem as Cliente)?.Id ?? 0;
        _agendamento.DataEvento = _data.Value.Date;
        _agendamento.Desconto = _desconto.Value;
        _agendamento.ValorPago = _valorPago.Value;
        _agendamento.Observacoes = _observacoes.Text.Trim();
        _agendamento.GarantirValido();

        var conflitos = _servico.BuscarConflitos(_agendamento);
        if (conflitos.Count > 0)
        {
            var nomes = conflitos.Select(a => _banco.Clientes.ObterPorId(a.ClienteId)?.Nome ?? "?");
            if (!Ui.Confirmar($"Já existe agendamento em {_data.Value:dd/MM/yyyy} para: {string.Join(", ", nomes)}.\n\nDeseja salvar mesmo assim?"))
                return;
        }

        _servico.Salvar(_agendamento);
        DialogResult = DialogResult.OK;
        Close();
    }
}
