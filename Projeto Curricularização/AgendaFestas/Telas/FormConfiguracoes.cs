using AgendaFestas.Dados;
using AgendaFestas.Modelos;

namespace AgendaFestas.Telas;

/// <summary>Dados da empresa impressos no cabeçalho do catálogo e dos relatórios.</summary>
public class FormConfiguracoes : FormBase
{
    private readonly BancoDados _banco;
    private readonly TextBox _nomeEmpresa = new();
    private readonly TextBox _telefone = new();
    private readonly TextBox _endereco = new();
    private readonly TextBox _observacao = new() { Multiline = true, Height = 70 };

    public FormConfiguracoes(BancoDados banco)
    {
        _banco = banco;
        Text = "Dados da empresa";
        Size = new Size(720, 420);
        MinimumSize = Size;

        var config = banco.ObterConfiguracao();
        var formulario = Ui.Formulario();
        Ui.Campo(formulario, "Nome da empresa", _nomeEmpresa).Text = config.NomeEmpresa;
        Ui.Campo(formulario, "Telefone", _telefone).Text = config.Telefone;
        Ui.Campo(formulario, "Endereço", _endereco).Text = config.Endereco;
        Ui.Campo(formulario, "Observação do catálogo", _observacao).Text = config.ObservacaoCatalogo;

        var info = new Label
        {
            Text = $"Os dados do sistema ficam na pasta:\n{banco.Pasta}\nFaça cópia dessa pasta para ter backup.",
            AutoSize = true,
            ForeColor = Color.DimGray
        };

        Ui.Montar(this, new Panel(),
            topo: [Ui.Titulo("Dados da empresa"), formulario, info],
            rodape: [Ui.BarraBotoes(Ui.Botao("Salvar", (_, _) => Salvar(), destaque: true), Ui.Botao("Cancelar", (_, _) => Close()))]);
    }

    private void Salvar()
    {
        if (string.IsNullOrWhiteSpace(_nomeEmpresa.Text))
            throw new ValidacaoException("Informe o nome da empresa.");

        _banco.SalvarConfiguracao(new Configuracao
        {
            NomeEmpresa = _nomeEmpresa.Text.Trim(),
            Telefone = _telefone.Text.Trim(),
            Endereco = _endereco.Text.Trim(),
            ObservacaoCatalogo = _observacao.Text.Trim()
        });
        DialogResult = DialogResult.OK;
        Close();
    }
}
