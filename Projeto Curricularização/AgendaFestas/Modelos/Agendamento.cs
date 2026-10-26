using System.Text.Json.Serialization;

namespace AgendaFestas.Modelos;

public class Agendamento : Entidade
{
    public int ClienteId { get; set; }
    public DateTime DataEvento { get; set; } = DateTime.Today;
    public List<ItemAgendamento> Itens { get; set; } = new();
    public decimal ValorPago { get; set; }
    public string Observacoes { get; set; } = "";
    public DateTime DataCadastro { get; set; } = DateTime.Now;

    [JsonIgnore]
    public decimal ValorTotal => Itens.Sum(i => i.Subtotal);

    [JsonIgnore]
    public decimal Saldo => ValorTotal - ValorPago;

    [JsonIgnore]
    public string SituacaoPagamento =>
        ValorPago <= 0 ? "Pendente" :
        Saldo <= 0 ? "Pago" :
        "Parcial";

    [JsonIgnore]
    public string ResumoServicos => string.Join(", ", Itens.Select(i => i.Descricao));

    public override IEnumerable<string> Validar()
    {
        if (ClienteId <= 0)
            yield return "Selecione o cliente.";
        if (Itens.Count == 0)
            yield return "Adicione pelo menos um serviço.";
        if (Itens.Any(i => i.Quantidade <= 0))
            yield return "A quantidade dos serviços deve ser maior que zero.";
        if (Itens.Any(i => i.ValorUnitario < 0))
            yield return "O valor dos serviços não pode ser negativo.";
        if (ValorPago < 0)
            yield return "O valor pago não pode ser negativo.";
        if (ValorPago > ValorTotal)
            yield return "O valor pago é maior que o valor total do agendamento.";
    }
}
