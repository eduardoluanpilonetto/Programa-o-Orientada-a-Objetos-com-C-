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

    /// <summary>Atualiza os valores unitários dos itens com a tabela de preços para a data informada.</summary>
    public void RecalcularPelaTabela(IEnumerable<ItemAgendamento> itens, DateTime data)
    {
        var tipos = _banco.TiposServico.ObterTodos();
        foreach (var item in itens)
        {
            var variacao = tipos.FirstOrDefault(t => t.Id == item.TipoServicoId)?.BuscarVariacao(item.Variacao);
            if (variacao is not null)
                item.ValorUnitario = PrecoSugerido(variacao, data);
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
        if (agendamento.Id == 0)
            _banco.Agendamentos.Adicionar(agendamento);
        else
            _banco.Agendamentos.Atualizar(agendamento);
    }

    public void Excluir(int id) => _banco.Agendamentos.Remover(id);
}
