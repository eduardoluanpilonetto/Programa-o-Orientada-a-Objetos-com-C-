using System.ComponentModel;

namespace AgendaFestas.Modelos;

public class Cliente : Entidade
{
    public string Nome { get; set; } = "";

    [DisplayName("Endereço")]
    public string Endereco { get; set; } = "";

    public string Telefone { get; set; } = "";

    [DisplayName("CPF")]
    public string Cpf { get; set; } = "";

    public override IEnumerable<string> Validar()
    {
        if (string.IsNullOrWhiteSpace(Nome))
            yield return "Informe o nome do cliente.";
        if (string.IsNullOrWhiteSpace(Endereco))
            yield return "Informe o endereço do cliente.";
        if (string.IsNullOrWhiteSpace(Telefone))
            yield return "Informe o telefone do cliente.";
        if (!string.IsNullOrWhiteSpace(Cpf) && !ValidadorCpf.EhValido(Cpf))
            yield return "CPF inválido.";
    }

    public override string ToString() => Nome;
}
