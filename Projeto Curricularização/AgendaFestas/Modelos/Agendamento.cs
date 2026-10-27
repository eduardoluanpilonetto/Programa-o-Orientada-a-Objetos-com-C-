using System.Text.Json.Serialization;

namespace AgendaFestas.Modelos;

public class Agendamento : Entidade
{
    public int ClienteId { get; set; }
    public DateTime DataEvento { get; set; } = DateTime.Today;
    public List<ItemAgendamento> Itens { get; set; } = new();
    public decimal Desconto { get; set; }
    public decimal ValorPago { get; set; }
    public string Observacoes { get; set; } = "";
    public DateTime DataCadastro { get; set; } = DateTime.Now;

    /// <summary>Soma dos serviços, antes do desconto.</summary>
    [JsonIgnore]
    public decimal ValorServicos => Itens.Sum(i => i.Subtotal);

    [JsonIgnore]
    public decimal ValorTotal => ValorServicos - Desconto;

    [JsonIgnore]
    public decimal Saldo => ValorTotal - ValorPago;

    [JsonIgnore]
    public string SituacaoPagamento =>
        ValorPago <= 0 ? "Pendente" :
        Saldo <= 0 ? "Pago" :
        "Parcial";

    [JsonIgnore]
    public string ResumoServicos => string.Join(", ", Itens.Select(i => i.Descricao));

    /// <summary>
    /// Cada tipo de serviço entra uma única vez: se já existir (mesmo com outra opção),
    /// o item antigo é substituído pelo novo. Retorna true quando houve substituição.
    /// </summary>
    public bool AdicionarServico(ItemAgendamento novo)
    {
        int indice = Itens.FindIndex(i => i.TipoServicoId == novo.TipoServicoId);
        if (indice < 0)
        {
            Itens.Add(novo);
            return false;
        }
        Itens[indice] = novo;
        return true;
    }

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
        if (Itens.GroupBy(i => i.TipoServicoId).Any(g => g.Count() > 1))
            yield return "O mesmo serviço foi adicionado mais de uma vez.";
        if (Desconto < 0)
            yield return "O desconto não pode ser negativo.";
        if (Desconto > ValorServicos)
            yield return "O desconto é maior que o valor dos serviços.";
        if (ValorPago < 0)
            yield return "O valor pago não pode ser negativo.";
        if (ValorPago > ValorTotal)
            yield return "O valor pago é maior que o valor total do agendamento.";
    }
}
