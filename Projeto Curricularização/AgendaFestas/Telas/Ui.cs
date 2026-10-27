using System.Diagnostics;

namespace AgendaFestas.Telas;

/// <summary>Atalhos para montar as telas por código com aparência consistente.</summary>
public static class Ui
{
    public static readonly Color CorPrincipal = Color.FromArgb(81, 45, 168);

    public static DataGridView CriarGrade(bool somenteLeitura = true)
    {
        var grade = new DataGridView
        {
            Dock = DockStyle.Fill,
            ReadOnly = somenteLeitura,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeRows = false,
            MultiSelect = false,
            SelectionMode = somenteLeitura ? DataGridViewSelectionMode.FullRowSelect : DataGridViewSelectionMode.CellSelect,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            RowHeadersVisible = false,
            BackgroundColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
            EnableHeadersVisualStyles = false
        };
        grade.ColumnHeadersDefaultCellStyle.BackColor = CorPrincipal;
        grade.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        grade.ColumnHeadersDefaultCellStyle.SelectionBackColor = CorPrincipal;
        grade.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
        grade.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 243, 250);
        grade.DataBindingComplete += (_, _) => FormatarColunas(grade);
        // célula apagada: texto vira "" e número vira 0, em vez de null (que gerava erro)
        grade.CellParsing += (_, e) =>
        {
            if (e.Value is string texto && string.IsNullOrWhiteSpace(texto) && e.DesiredType is not null)
            {
                e.Value = e.DesiredType == typeof(string) ? "" : Activator.CreateInstance(e.DesiredType);
                e.ParsingApplied = true;
            }
        };
        grade.DataError += (_, e) =>
        {
            Aviso("Valor inválido. Digite apenas números (ex.: 150,00).");
            e.ThrowException = false;
        };
        return grade;
    }

    private static void FormatarColunas(DataGridView grade)
    {
        foreach (DataGridViewColumn coluna in grade.Columns)
        {
            if (coluna.ValueType == typeof(decimal))
            {
                coluna.DefaultCellStyle.Format = "C2";
                coluna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            else if (coluna.ValueType == typeof(DateTime))
            {
                coluna.DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            else if (coluna.ValueType == typeof(int))
            {
                coluna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        if (grade.Columns["Servicos"] is { } servicos)
            servicos.FillWeight = 250;
    }

    /// <summary>
    /// Distribui os controles na tela: "topo" de cima para baixo, "rodape" de cima para baixo,
    /// "direita" como painel lateral e o "centro" ocupa o espaço que sobrar.
    /// (O WinForms encaixa os controles na ordem inversa em que são adicionados, por isso a ordem abaixo.)
    /// </summary>
    public static void Montar(Control pai, Control centro, Control[]? topo = null, Control[]? rodape = null, Control? direita = null)
    {
        centro.Dock = DockStyle.Fill;
        pai.Controls.Add(centro);
        if (direita is not null)
        {
            direita.Dock = DockStyle.Right;
            pai.Controls.Add(direita);
        }
        foreach (var controle in (topo ?? []).Reverse())
        {
            controle.Dock = DockStyle.Top;
            pai.Controls.Add(controle);
        }
        foreach (var controle in rodape ?? [])
        {
            controle.Dock = DockStyle.Bottom;
            pai.Controls.Add(controle);
        }
    }

    public static T? Selecionado<T>(DataGridView grade) where T : class
    {
        var linha = grade.SelectedRows.Count > 0 ? grade.SelectedRows[0] : grade.CurrentRow;
        return linha?.DataBoundItem as T;
    }

    public static Button Botao(string texto, EventHandler aoClicar, bool destaque = false)
    {
        var botao = new Button
        {
            Text = texto,
            AutoSize = true,
            Padding = new Padding(10, 4, 10, 4),
            Margin = new Padding(0, 0, 8, 0),
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
        };
        if (destaque)
        {
            botao.BackColor = CorPrincipal;
            botao.ForeColor = Color.White;
        }
        botao.FlatAppearance.BorderColor = CorPrincipal;
        botao.Click += aoClicar;
        return botao;
    }

    public static FlowLayoutPanel BarraBotoes(params Control[] controles)
    {
        var barra = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            AutoSize = true,
            Padding = new Padding(0, 10, 0, 0),
            WrapContents = false
        };
        barra.Controls.AddRange(controles);
        return barra;
    }

    public static Label Titulo(string texto) => new()
    {
        Text = texto,
        Dock = DockStyle.Top,
        AutoSize = false,
        Height = 40,
        Font = new Font("Segoe UI", 15f, FontStyle.Bold),
        ForeColor = CorPrincipal
    };

    /// <summary>Tabela de 2 colunas (rótulo | campo) para formulários.</summary>
    public static TableLayoutPanel Formulario()
    {
        var tabela = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            ColumnCount = 2,
            Padding = new Padding(0, 0, 0, 8)
        };
        tabela.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        tabela.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        return tabela;
    }

    public static T Campo<T>(TableLayoutPanel formulario, string rotulo, T controle) where T : Control
    {
        formulario.Controls.Add(new Label
        {
            Text = rotulo,
            AutoSize = true,
            Anchor = AnchorStyles.Left,
            Margin = new Padding(0, 6, 10, 6)
        });
        controle.Dock = DockStyle.Fill;
        controle.Margin = new Padding(0, 3, 0, 3);
        formulario.Controls.Add(controle);
        return controle;
    }

    public static NumericUpDown CampoValor() => new()
    {
        DecimalPlaces = 2,
        Maximum = 1_000_000,
        ThousandsSeparator = true,
        TextAlign = HorizontalAlignment.Right,
        Width = 130
    };

    public static DateTimePicker CampoData(DateTime valor) => new()
    {
        Format = DateTimePickerFormat.Short,
        Value = valor,
        Width = 130
    };

    public static Label RotuloSimples(string texto) => new()
    {
        Text = texto,
        AutoSize = true,
        Margin = new Padding(0, 7, 6, 0)
    };

    public static bool Confirmar(string mensagem) =>
        MessageBox.Show(mensagem, "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

    public static void Info(string mensagem) =>
        MessageBox.Show(mensagem, "Agenda de Festas", MessageBoxButtons.OK, MessageBoxIcon.Information);

    public static void Aviso(string mensagem) =>
        MessageBox.Show(mensagem, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);

    /// <summary>Pergunta onde salvar o PDF, gera e abre o arquivo.</summary>
    public static void SalvarEAbrirPdf(string nomeSugerido, Action<string> gerar)
    {
        using var dialogo = new SaveFileDialog
        {
            Filter = "Arquivo PDF (*.pdf)|*.pdf",
            FileName = nomeSugerido,
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        };
        if (dialogo.ShowDialog() != DialogResult.OK)
            return;

        Cursor.Current = Cursors.WaitCursor;
        try
        {
            gerar(dialogo.FileName);
        }
        catch (IOException)
        {
            Aviso("Não foi possível salvar o PDF. Se ele estiver aberto em outro programa, feche-o e tente novamente.");
            return;
        }
        finally
        {
            Cursor.Current = Cursors.Default;
        }
        Process.Start(new ProcessStartInfo(dialogo.FileName) { UseShellExecute = true });
    }
}
