using System.ComponentModel;
using AgendaFestas.Dados;
using AgendaFestas.Modelos;

namespace AgendaFestas.Telas;

public class FormTiposServico : FormBase
{
    private readonly BancoDados _banco;
    private readonly DataGridView _grade = Ui.CriarGrade();
    private readonly DataGridView _gradeVariacoes = Ui.CriarGrade(somenteLeitura: false);
    private readonly TextBox _nome = new() { MaxLength = 100 };
    private readonly TextBox _descricao = new() { Multiline = true, Height = 60, ScrollBars = ScrollBars.Vertical };
    private BindingList<VariacaoServico> _variacoes = new();
    private int _idAtual;

    public FormTiposServico(BancoDados banco)
    {
        _banco = banco;
        Text = "Tipos de serviço";
        Size = new Size(1250, 650);

        var formulario = Ui.Formulario();
        Ui.Campo(formulario, "Nome *", _nome);
        Ui.Campo(formulario, "Descrição", _descricao);

        _gradeVariacoes.AllowUserToAddRows = true;
        _gradeVariacoes.DataBindingComplete += (_, _) =>
        {
            foreach (DataGridViewColumn coluna in _gradeVariacoes.Columns)
            {
                if (coluna.ValueType == typeof(decimal))
                    coluna.DefaultCellStyle.Format = "N2"; // sem "R$" para caber na coluna
                else
                    coluna.FillWeight = 260;
            }
        };

        var ajuda = new Label
        {
            Text = "Variações e preços por dia (ex.: Simples - Pequena, Luxo - Grande). Para adicionar, digite na última linha " +
                   "e clique em Salvar. Sem variações = valor informado no agendamento.",
            AutoSize = false,
            Height = 50,
            ForeColor = Color.DimGray
        };

        var botoesVariacao = Ui.BarraBotoes(
            Ui.Botao("Remover variação", (_, _) => RemoverVariacao()),
            Ui.Botao("Mesmo preço todos os dias", (_, _) => RepetirPrecoSegunda()));
        botoesVariacao.Padding = new Padding(0, 6, 0, 0);

        var botoesServico = Ui.BarraBotoes(
            Ui.Botao("Novo serviço", (_, _) => Novo()),
            Ui.Botao("Salvar", (_, _) => Salvar(), destaque: true),
            Ui.Botao("Excluir serviço", (_, _) => Excluir()));
        botoesServico.Padding = new Padding(0, 14, 0, 0);

        var painelDados = new Panel { Width = 820, Padding = new Padding(16, 0, 0, 0) };
        Ui.Montar(painelDados, _gradeVariacoes,
            topo: [Ui.Titulo("Dados do serviço"), formulario, ajuda],
            rodape: [botoesVariacao, botoesServico]);

        Ui.Montar(this, _grade, topo: [Ui.Titulo("Tipos de serviço")], direita: painelDados);

        _grade.SelectionChanged += (_, _) =>
        {
            if (Ui.Selecionado<TipoServico>(_grade) is { } tipo)
                Preencher(tipo);
        };
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        Carregar();
    }

    private void Carregar(int? selecionarId = null)
    {
        _grade.DataSource = _banco.TiposServico.ObterTodos().OrderBy(t => t.Nome).ToList();
        _grade.CurrentCell = null;

        foreach (DataGridViewRow linha in _grade.Rows)
        {
            if (linha.DataBoundItem is TipoServico t && t.Id == selecionarId)
            {
                _grade.CurrentCell = linha.Cells[0];
                return;
            }
        }
        Limpar();
    }

    private void Preencher(TipoServico tipo)
    {
        _idAtual = tipo.Id;
        _nome.Text = tipo.Nome;
        _descricao.Text = tipo.Descricao;
        _variacoes = new BindingList<VariacaoServico>(tipo.Variacoes);
        _gradeVariacoes.DataSource = _variacoes;
    }

    private void Limpar()
    {
        _idAtual = 0;
        _nome.Clear();
        _descricao.Clear();
        _variacoes = new BindingList<VariacaoServico>();
        _gradeVariacoes.DataSource = _variacoes;
    }

    private void Novo()
    {
        _grade.CurrentCell = null;
        Limpar();
        _nome.Focus();
    }

    /// <summary>Copia o preço de segunda-feira para os demais dias da variação selecionada.</summary>
    private void RepetirPrecoSegunda()
    {
        _gradeVariacoes.EndEdit();
        if (Ui.Selecionado<VariacaoServico>(_gradeVariacoes) is not { } v)
        {
            Ui.Aviso("Clique em uma variação da tabela e preencha o preço de segunda-feira.");
            return;
        }
        v.PrecoTerca = v.PrecoQuarta = v.PrecoQuinta = v.PrecoSexta = v.PrecoSabado = v.PrecoDomingo = v.PrecoSegunda;
        _gradeVariacoes.Refresh();
    }

    /// <summary>Remove só a variação selecionada (o serviço continua) e grava na hora.</summary>
    private void RemoverVariacao()
    {
        _gradeVariacoes.EndEdit();
        if (Ui.Selecionado<VariacaoServico>(_gradeVariacoes) is not { } variacao)
        {
            Ui.Aviso("Clique na variação que deseja remover.");
            return;
        }
        string nome = string.IsNullOrWhiteSpace(variacao.Nome) ? "sem nome" : $"\"{variacao.Nome}\"";
        if (!Ui.Confirmar($"Remover a variação {nome} do serviço \"{_nome.Text}\"?"))
            return;

        _variacoes.Remove(variacao);
        if (_idAtual != 0)
            Salvar(mostrarMensagem: false);
    }

    private void Salvar(bool mostrarMensagem = true)
    {
        _gradeVariacoes.EndEdit();

        // ignora linhas que ficaram totalmente em branco
        var variacoes = _variacoes
            .Where(v => !string.IsNullOrWhiteSpace(v.Nome) || v.Precos().Any(p => p != 0))
            .ToList();

        var tipo = new TipoServico
        {
            Id = _idAtual,
            Nome = _nome.Text.Trim(),
            Descricao = _descricao.Text.Trim(),
            Variacoes = variacoes
        };

        if (tipo.Id == 0)
            _banco.TiposServico.Adicionar(tipo);
        else
            _banco.TiposServico.Atualizar(tipo);

        Carregar(tipo.Id);
        if (mostrarMensagem)
            Ui.Info("Serviço salvo com sucesso.");
    }

    private void Excluir()
    {
        if (_idAtual == 0)
        {
            Ui.Aviso("Selecione um serviço na lista.");
            return;
        }
        if (!Ui.Confirmar($"Excluir o serviço \"{_nome.Text}\" inteiro, com todas as variações?\n\nAgendamentos já feitos com ele não serão alterados."))
            return;

        _banco.TiposServico.Remover(_idAtual);
        Carregar();
    }
}
