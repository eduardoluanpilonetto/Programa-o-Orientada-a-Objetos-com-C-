namespace AgendaFestas.Telas;

/// <summary>Visual padrão de todas as janelas do sistema.</summary>
public class FormBase : Form
{
    public FormBase()
    {
        Font = new Font("Segoe UI", 10f);
        StartPosition = FormStartPosition.CenterParent;
        BackColor = Color.White;
        Padding = new Padding(12);
        MinimumSize = new Size(700, 450);
        KeyPreview = true;
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape && Modal)
            Close();
        base.OnKeyDown(e);
    }
}
