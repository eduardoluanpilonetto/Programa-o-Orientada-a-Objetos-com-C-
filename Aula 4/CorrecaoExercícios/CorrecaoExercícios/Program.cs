using ExerciciosAula4;
using System;


class Program
{
    static void Main(string[] args)
    {
        // Exercício 1 – Construtor Básico
        Pessoa pessoa1 = new Pessoa();
        pessoa1.ExibirDados();

        // Exercício 2 – Construtor com Parâmetros
        ProdutoSimples produto1 = new ProdutoSimples("Teclado Mecânico", 250.00);
        ProdutoSimples produto2 = new ProdutoSimples("Mouse Gamer", 120.50);
        produto1.ExibirDados();
        produto2.ExibirDados();

        // Exercício 3 – Sobrecarga de Construtores
        Aluno aluno1 = new Aluno();
        Aluno aluno2 = new Aluno("Carlos");
        Aluno aluno3 = new Aluno("Mariana", "Engenharia de Software", 9.5);
        aluno1.ExibirDados();
        aluno2.ExibirDados();
        aluno3.ExibirDados();

        // Exercício 4 – Uso do this
        Cliente cliente1 = new Cliente("João Pereira", "Porto Alegre");
        cliente1.ExibirDados();

        // Exercício 5 – Inicialização Automática
        ContaBancaria conta1 = new ContaBancaria("12345-6", "Ana Souza");
        conta1.ExibirSaldo();

        // Exercício 6 – Construtor Chamando Outro Construtor
        Funcionario funcionario1 = new Funcionario();
        Funcionario funcionario2 = new Funcionario("Pedro Lima", "Analista de Sistemas", 4500.00);
        funcionario1.ExibirDados();
        funcionario2.ExibirDados();

        // Exercício 7 – Cadastro de Veículos
        Veiculo veiculo1 = new Veiculo();
        Veiculo veiculo2 = new Veiculo("Fiat", "Argo", 2022);
        Veiculo veiculo3 = new Veiculo("Toyota", "Corolla", 2023);
        veiculo1.ExibirDados();
        veiculo2.ExibirDados();
        veiculo3.ExibirDados();

        // Exercício 8 – Destrutor
        ArquivoTemporario arquivo1 = new ArquivoTemporario();
        arquivo1.ExibirInfo();

        // Exercício 9 – Sistema de Biblioteca
        Livro livro1 = new Livro();
        Livro livro2 = new Livro("Clean Code", "Robert C. Martin", 464);
        livro1.ExibirDados();
        livro2.ExibirDados();

        // Exercício 10 – Desafio Completo (Cadastro de Produtos)
        ProdutoCadastro produtoCad1 = new ProdutoCadastro();
        ProdutoCadastro produtoCad2 = new ProdutoCadastro(101, "Notebook Dell", 3800.00);
        produtoCad1.ExibirDados();
        produtoCad2.ExibirDados();
    }
}