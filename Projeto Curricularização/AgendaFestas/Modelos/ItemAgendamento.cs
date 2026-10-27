using System.ComponentModel;
using System.Text.Json.Serialization;

namespace AgendaFestas.Modelos;

/// <summary>
/// Um serviço contratado dentro de um agendamento.
/// Guarda uma "foto" do nome e do valor no momento do agendamento,
/// assim mudar a tabela de preços depois não altera festas já fechadas.
/// </summary>
public class ItemAgendamento
{
    [Browsable(false)]
    public int TipoServicoId { get; set; }

    [DisplayName("Serviço")]
    public string NomeServico { get; set; } = "";

    [DisplayName("Variação")]
    public string Variacao { get; set; } = "";

    [DisplayName("Qtde")]
    public int Quantidade { get; set; } = 1;

    [DisplayName("Valor unitário")]
    public decimal ValorUnitario { get; set; }

    [JsonIgnore]
    public decimal Subtotal => Quantidade * ValorUnitario;

    [JsonIgnore, Browsable(false)]
    public string Descricao
    {
        get
        {
            string texto = string.IsNullOrWhiteSpace(Variacao) ? NomeServico : $"{NomeServico} ({Variacao})";
            return Quantidade > 1 ? $"{Quantidade}x {texto}" : texto;
        }
    }
}
