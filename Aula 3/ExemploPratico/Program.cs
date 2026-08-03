using System;

class Program
{
    static void Main(string[] args)
    {
        Pessoa pessoa = new Pessoa("João", 25, "123.456.789-00");

        pessoa.Nome = "Maria";
        pessoa.Idade = 30;

        // As linhas abaixo gerariam erro, pois o setter é privado:
        // pessoa.CPF = "999.999.999-99";
        // pessoa.DataCadastro = DateTime.Now;

        pessoa.ExibirDados();
    }
}


public class Pessoa
{
    private string nome;
    private int idade;
    private string cpf;
    private DateTime dataCadastro;

    public string Nome
    {
        get { return nome; }
        set { nome = value; }
    }

    public int Idade
    {
        get { return idade; }
        set
        {
            if (value >= 0)
            {
                idade = value;
            }
        }
    }

    public string CPF
    {
        get { return cpf; }
        private set { cpf = value; } // Setter privado
    }
    public DateTime DataCadastro
    {
        get { return dataCadastro; }
        private set { dataCadastro = value; } // Setter privado
    }
    public Pessoa(string nome, int idade, string cpf)
    {
        Nome = nome;
        Idade = idade;
        CPF = cpf;
        DataCadastro = DateTime.Now;
    }
    public void ExibirDados()
    {
        Console.WriteLine($"Nome: {Nome}");
        Console.WriteLine($"Idade: {Idade}");
        Console.WriteLine($"CPF: {CPF}");
        Console.WriteLine($"Data de Cadastro: {DataCadastro}");
    }
}
