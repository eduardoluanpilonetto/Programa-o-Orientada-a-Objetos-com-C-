namespace AgendaFestas.Modelos;

public static class ValidadorTelefone
{
    /// <summary>DDD + 8 dígitos (fixo) ou DDD + 9 dígitos (celular).</summary>
    public static bool EhValido(string telefone)
    {
        int digitos = telefone.Count(char.IsDigit);
        return (digitos == 10 || digitos == 11) && !telefone.Any(char.IsLetter);
    }
}
