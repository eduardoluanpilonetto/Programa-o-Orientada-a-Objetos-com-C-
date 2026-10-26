namespace AgendaFestas.Modelos;

public static class ValidadorCpf
{
    public static bool EhValido(string cpf)
    {
        var digitos = new string(cpf.Where(char.IsDigit).ToArray());
        if (digitos.Length != 11 || digitos.Distinct().Count() == 1)
            return false;

        return digitos[9] - '0' == CalcularDigito(digitos, 9)
            && digitos[10] - '0' == CalcularDigito(digitos, 10);
    }

    private static int CalcularDigito(string digitos, int quantidade)
    {
        int soma = 0;
        for (int i = 0; i < quantidade; i++)
            soma += (digitos[i] - '0') * (quantidade + 1 - i);

        int resto = soma % 11;
        return resto < 2 ? 0 : 11 - resto;
    }
}
