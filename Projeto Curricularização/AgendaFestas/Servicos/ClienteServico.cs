using AgendaFestas.Dados;
using AgendaFestas.Modelos;

namespace AgendaFestas.Servicos;

public class ClienteServico
{
    private readonly BancoDados _banco;

    public ClienteServico(BancoDados banco) => _banco = banco;

    public List<Cliente> Listar(string filtro = "")
    {
        var clientes = _banco.Clientes.ObterTodos().AsEnumerable();
        if (!string.IsNullOrWhiteSpace(filtro))
        {
            clientes = clientes.Where(c =>
                c.Nome.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                c.Telefone.Contains(filtro) ||
                c.Cpf.Contains(filtro));
        }
        return clientes.OrderBy(c => c.Nome).ToList();
    }

    public void Salvar(Cliente cliente)
    {
        bool cpfRepetido = !string.IsNullOrWhiteSpace(cliente.Cpf) && _banco.Clientes.ObterTodos()
            .Any(c => c.Id != cliente.Id && c.Cpf == cliente.Cpf);
        if (cpfRepetido)
            throw new ValidacaoException("Já existe um cliente cadastrado com este CPF.");

        if (cliente.Id == 0)
            _banco.Clientes.Adicionar(cliente);
        else
            _banco.Clientes.Atualizar(cliente);
    }

    public void Excluir(int id)
    {
        if (_banco.Agendamentos.ObterTodos().Any(a => a.ClienteId == id))
            throw new ValidacaoException("Este cliente possui agendamentos e não pode ser excluído.");

        _banco.Clientes.Remover(id);
    }
}
