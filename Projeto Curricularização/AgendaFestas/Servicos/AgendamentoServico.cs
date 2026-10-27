using AgendaFestas.Dados;
using AgendaFestas.Modelos;

namespace AgendaFestas.Servicos;

public class AgendamentoServico
{
    private readonly BancoDados _banco;

    public AgendamentoServico(BancoDados banco) => _banco = banco;

    /// <summary>Preço de tabela de uma variação para a data do evento.</summary>
    public static decimal PrecoSugerido(VariacaoServico? variacao, DateTime data) =>
        variacao?.ObterPreco(data.DayOfWeek) ?? 0;

    /// <summary>
    /// Quando a data do evento muda, troca o valor dos itens que estavam com o preço de tabela
    /// pelo preço do novo dia da semana. Valores digitados à mão (diferentes da tabela) são mantidos.
    /// </summary>
    public void AtualizarPrecosParaNovaData(IEnumerable<ItemAgendamento> itens, DateTime dataAnterior, DateTime novaData)
    {
        var tipos = _banco.TiposServico.ObterTodos();
        foreach (var item in itens)
        {
            var variacao = tipos.FirstOrDefault(t => t.Id == item.TipoServicoId)?.BuscarVariacao(item.Variacao);
            if (variacao is not null && item.ValorUnitario == PrecoSugerido(variacao, dataAnterior))
                item.ValorUnitario = PrecoSugerido(variacao, novaData);
        }
    }

    /// <summary>Outros agendamentos já marcados para o mesmo dia.</summary>
    public List<Agendamento> BuscarConflitos(Agendamento agendamento) =>
        _banco.Agendamentos.ObterTodos()
            .Where(a => a.Id != agendamento.Id && a.DataEvento.Date == agendamento.DataEvento.Date)
            .ToList();

    public void Salvar(Agendamento agendamento)
    {
        if (agendamento.ClienteId > 0 && _banco.Clientes.ObterPorId(agendamento.ClienteId) is null)
            throw new ValidacaoException("O cliente selecionado não existe mais.");

        agendamento.DataEvento = agendamento.DataEvento.Date;

        // Festa já realizada pode ser editada (ex.: registrar pagamento), mas não dá para marcar uma data que já passou.
        var original = agendamento.Id == 0 ? null : _banco.Agendamentos.ObterPorId(agendamento.Id);
        bool dataAlterada = original is null || original.DataEvento.Date != agendamento.DataEvento;
        if (dataAlterada && agendamento.DataEvento < DateTime.Today)
            throw new ValidacaoException("Não é possível agendar para uma data que já passou.");

        if (agendamento.Id == 0)
            _banco.Agendamentos.Adicionar(agendamento);
        else
            _banco.Agendamentos.Atualizar(agendamento);
    }

    public void Excluir(int id) => _banco.Agendamentos.Remover(id);
}
