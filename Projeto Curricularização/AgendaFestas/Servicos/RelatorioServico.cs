using System.ComponentModel;
using AgendaFestas.Dados;
using AgendaFestas.Modelos;

namespace AgendaFestas.Servicos;

/// <summary>Linha pronta para exibir um agendamento em grade ou PDF.</summary>
public class LinhaAgendamento
{
    [Browsable(false)] public int Id { get; init; }
    [Browsable(false)] public int ClienteId { get; init; }
    public DateTime Data { get; init; }
    [DisplayName("Dia")] public string DiaSemana { get; init; } = "";
    public string Cliente { get; init; } = "";
    public string Telefone { get; init; } = "";
    [DisplayName("Serviços")] public string Servicos { get; init; } = "";
    [DisplayName("Valor total")] public decimal ValorTotal { get; init; }
    [DisplayName("Valor pago")] public decimal ValorPago { get; init; }
    [DisplayName("A receber")] public decimal Saldo { get; init; }
    [DisplayName("Situação")] public string Situacao { get; init; } = "";
}

/// <summary>Totais financeiros de um cliente (relatório de caixa).</summary>
public class ResumoCaixaCliente
{
    [Browsable(false)] public int ClienteId { get; init; }
    public string Cliente { get; init; } = "";
    public string Telefone { get; init; } = "";
    [DisplayName("Agendamentos")] public int Quantidade => Agendamentos.Count;
    [DisplayName("Total contratado")] public decimal TotalContratado => Agendamentos.Sum(a => a.ValorTotal);
    [DisplayName("Total pago")] public decimal TotalPago => Agendamentos.Sum(a => a.ValorPago);
    [DisplayName("A receber")] public decimal AReceber => Agendamentos.Sum(a => a.Saldo);
    [Browsable(false)] public List<LinhaAgendamento> Agendamentos { get; init; } = new();
}

public class RelatorioServico
{
    private readonly BancoDados _banco;

    public RelatorioServico(BancoDados banco) => _banco = banco;

    public List<LinhaAgendamento> ListarAgendamentos(DateTime de, DateTime ate, int? clienteId = null)
    {
        var clientes = _banco.Clientes.ObterTodos().ToDictionary(c => c.Id);

        return _banco.Agendamentos.ObterTodos()
            .Where(a => a.DataEvento.Date >= de.Date && a.DataEvento.Date <= ate.Date)
            .Where(a => clienteId is null || a.ClienteId == clienteId)
            .OrderBy(a => a.DataEvento)
            .Select(a =>
            {
                clientes.TryGetValue(a.ClienteId, out var cliente);
                return new LinhaAgendamento
                {
                    Id = a.Id,
                    ClienteId = a.ClienteId,
                    Data = a.DataEvento,
                    DiaSemana = VariacaoServico.NomeDia(a.DataEvento.DayOfWeek),
                    Cliente = cliente?.Nome ?? "(cliente removido)",
                    Telefone = cliente?.Telefone ?? "",
                    Servicos = a.ResumoServicos,
                    ValorTotal = a.ValorTotal,
                    ValorPago = a.ValorPago,
                    Saldo = a.Saldo,
                    Situacao = a.SituacaoPagamento
                };
            })
            .ToList();
    }

    public List<ResumoCaixaCliente> CaixaPorCliente(DateTime de, DateTime ate) =>
        ListarAgendamentos(de, ate)
            .GroupBy(l => l.ClienteId)
            .Select(g => new ResumoCaixaCliente
            {
                ClienteId = g.Key,
                Cliente = g.First().Cliente,
                Telefone = g.First().Telefone,
                Agendamentos = g.ToList()
            })
            .OrderBy(r => r.Cliente)
            .ToList();
}
