namespace AgendaFestas.Telas;

/// <summary>
/// Caixa de texto que só aceita números e aplica a máscara enquanto o usuário digita ou cola.
/// Ao colar "529.982.247-25" ou "52998224725", o resultado é o mesmo.
/// </summary>
public abstract class CampoFormatado : TextBox
{
    private bool _formatando;

    protected abstract int MaximoDigitos { get; }
    protected abstract string Formatar(string digitos);

    public string Digitos => new(Text.Where(char.IsDigit).ToArray());

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
        if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            e.Handled = true;
        base.OnKeyPress(e);
    }

    protected override void OnTextChanged(EventArgs e)
    {
        if (!_formatando)
        {
            _formatando = true;
            string digitos = Digitos;
            if (digitos.Length > MaximoDigitos)
                digitos = digitos[..MaximoDigitos];

            string formatado = digitos.Length == 0 ? "" : Formatar(digitos);
            if (Text != formatado)
            {
                Text = formatado;
                SelectionStart = Text.Length;
            }
            _formatando = false;
        }
        base.OnTextChanged(e);
    }
}

/// <summary>Telefone fixo (49) 3333-4444 ou celular (49) 99999-0000.</summary>
public class CampoTelefone : CampoFormatado
{
    protected override int MaximoDigitos => 11;

    protected override string Formatar(string d) => d.Length switch
    {
        <= 2 => $"({d}",
        <= 6 => $"({d[..2]}) {d[2..]}",
        <= 10 => $"({d[..2]}) {d[2..6]}-{d[6..]}",
        _ => $"({d[..2]}) {d[2..7]}-{d[7..]}"
    };
}

/// <summary>CPF 000.000.000-00.</summary>
public class CampoCpf : CampoFormatado
{
    protected override int MaximoDigitos => 11;

    protected override string Formatar(string d) => d.Length switch
    {
        <= 3 => d,
        <= 6 => $"{d[..3]}.{d[3..]}",
        <= 9 => $"{d[..3]}.{d[3..6]}.{d[6..]}",
        _ => $"{d[..3]}.{d[3..6]}.{d[6..9]}-{d[9..]}"
    };
}

/// <summary>Par de datas "De / Até" em que a data final nunca fica antes da inicial.</summary>
public class FiltroPeriodo : FlowLayoutPanel
{
    private readonly DateTimePicker _de;
    private readonly DateTimePicker _ate;

    public DateTime De => _de.Value.Date;
    public DateTime Ate => _ate.Value.Date;

    public FiltroPeriodo(string rotuloDe, DateTime de, DateTime ate)
    {
        AutoSize = true;
        WrapContents = false;
        Margin = Padding.Empty;

        _de = Ui.CampoData(de);
        _ate = Ui.CampoData(ate);
        // Ao mover a data inicial para depois da final, o DateTimePicker empurra a final junto.
        _ate.MinDate = _de.Value.Date;
        _de.ValueChanged += (_, _) => _ate.MinDate = _de.Value.Date;

        Controls.AddRange([Ui.RotuloSimples(rotuloDe), _de, Ui.RotuloSimples("Até:"), _ate]);
    }
}
