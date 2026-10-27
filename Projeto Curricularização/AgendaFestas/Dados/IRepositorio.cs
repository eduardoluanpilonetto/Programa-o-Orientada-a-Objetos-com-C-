using AgendaFestas.Modelos;

namespace AgendaFestas.Dados;

public interface IRepositorio<T> where T : Entidade
{
    List<T> ObterTodos();
    T? ObterPorId(int id);
    void Adicionar(T entidade);
    void Atualizar(T entidade);
    void Remover(int id);
}
