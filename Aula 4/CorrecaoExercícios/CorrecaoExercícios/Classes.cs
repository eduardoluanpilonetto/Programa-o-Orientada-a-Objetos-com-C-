using System;

namespace ExerciciosAula4
{
    // ==========================================================
    // Exercício 1 – Construtor Básico
    // ==========================================================
    public class Pessoa
    {
        public string Nome;
        public int Idade;

        // Construtor padrão
        public Pessoa()
        {
            Nome = "Não informado";
            Idade = 0;
        }

        public void ExibirDados()
        {
            Console.WriteLine($"[Pessoa] Nome: {Nome} | Idade: {Idade}");
        }
    }

    // ==========================================================
    // Exercício 2 – Construtor com Parâmetros
    // ==========================================================
    public class ProdutoSimples
    {
        public string Nome;
        public double Preco;

        public ProdutoSimples(string nome, double preco)
        {
            Nome = nome;
            Preco = preco;
        }

        public void ExibirDados()
        {
            Console.WriteLine($"[ProdutoSimples] Nome: {Nome} | Preço: {Preco:C}");
        }
    }

    // ==========================================================
    // Exercício 3 – Sobrecarga de Construtores
    // ==========================================================
    public class Aluno
    {
        public string Nome;
        public string Curso;
        public double NotaFinal;

        // Construtor sem parâmetros
        public Aluno()
        {
            Nome = "Não informado";
            Curso = "Não informado";
            NotaFinal = 0;
        }

        // Construtor apenas com nome
        public Aluno(string nome)
        {
            Nome = nome;
            Curso = "Não informado";
            NotaFinal = 0;
        }

        // Construtor com nome, curso e nota
        public Aluno(string nome, string curso, double notaFinal)
        {
            Nome = nome;
            Curso = curso;
            NotaFinal = notaFinal;
        }

        public void ExibirDados()
        {
            Console.WriteLine($"[Aluno] Nome: {Nome} | Curso: {Curso} | Nota Final: {NotaFinal}");
        }
    }

    // ==========================================================
    // Exercício 4 – Uso do this
    // ==========================================================
    public class Cliente
    {
        public string Nome;
        public string Cidade;

        public Cliente(string Nome, string Cidade)
        {
            this.Nome = Nome;
            this.Cidade = Cidade;
        }

        public void ExibirDados()
        {
            Console.WriteLine($"[Cliente] Nome: {Nome} | Cidade: {Cidade}");
        }
    }

    // ==========================================================
    // Exercício 5 – Inicialização Automática
    // ==========================================================
    public class ContaBancaria
    {
        public string NumeroConta;
        public string Titular;
        public double Saldo;

        public ContaBancaria(string numeroConta, string titular)
        {
            NumeroConta = numeroConta;
            Titular = titular;
            Saldo = 0; // Saldo sempre inicia em 0
        }

        public void ExibirSaldo()
        {
            Console.WriteLine($"[ContaBancaria] Conta: {NumeroConta} | Titular: {Titular} | Saldo Inicial: {Saldo:C}");
        }
    }

    // ==========================================================
    // Exercício 6 – Construtor Chamando Outro Construtor
    // ==========================================================
    public class Funcionario
    {
        public string Nome;
        public string Cargo;
        public double Salario;

        // Construtor padrão chama o construtor completo
        public Funcionario() : this("Não informado", "Não informado", 0)
        {
        }

        // Construtor completo
        public Funcionario(string nome, string cargo, double salario)
        {
            Nome = nome;
            Cargo = cargo;
            Salario = salario;
        }

        public void ExibirDados()
        {
            Console.WriteLine($"[Funcionario] Nome: {Nome} | Cargo: {Cargo} | Salário: {Salario:C}");
        }
    }

    // ==========================================================
    // Exercício 7 – Cadastro de Veículos
    // ==========================================================
    public class Veiculo
    {
        public string Marca;
        public string Modelo;
        public int Ano;

        // Construtor padrão
        public Veiculo()
        {
            Marca = "Não informado";
            Modelo = "Não informado";
            Ano = 0;
        }

        // Construtor com parâmetros
        public Veiculo(string marca, string modelo, int ano)
        {
            Marca = marca;
            Modelo = modelo;
            Ano = ano;
        }

        public void ExibirDados()
        {
            Console.WriteLine($"[Veiculo] Marca: {Marca} | Modelo: {Modelo} | Ano: {Ano}");
        }
    }

    // ==========================================================
    // Exercício 8 – Destrutor
    // ==========================================================
    public class ArquivoTemporario
    {
        public ArquivoTemporario()
        {
        }

        // Destrutor - chamado pelo Garbage Collector quando o objeto
        // não tem mais referências e é coletado (momento não determinístico)
        ~ArquivoTemporario()
        {
            Console.WriteLine("Objeto removido da memória");
        }

        public void ExibirInfo()
        {
            Console.WriteLine("[ArquivoTemporario] Objeto criado. O destrutor será executado " +
                "pelo Garbage Collector quando este objeto não tiver mais referências, " +
                "em um momento não determinístico (não é possível prever exatamente quando).");
        }
    }

    // ==========================================================
    // Exercício 9 – Sistema de Biblioteca
    // ==========================================================
    public class Livro
    {
        public string Titulo;
        public string Autor;
        public int QuantidadePaginas;

        // Construtor padrão
        public Livro()
        {
            Titulo = "Não informado";
            Autor = "Não informado";
            QuantidadePaginas = 0;
        }

        // Construtor completo (sobrecarga)
        public Livro(string titulo, string autor, int quantidadePaginas)
        {
            Titulo = titulo;
            Autor = autor;
            QuantidadePaginas = quantidadePaginas;
        }

        public void ExibirDados()
        {
            Console.WriteLine($"[Livro] Título: {Titulo} | Autor: {Autor} | Páginas: {QuantidadePaginas}");
        }
    }

    // ==========================================================
    // Exercício 10 – Desafio Completo (Cadastro de Produtos)
    // ==========================================================
    public class ProdutoCadastro
    {
        public int Codigo;
        public string Nome;
        public double Preco;
        public int Estoque;

        // Construtor padrão
        public ProdutoCadastro()
        {
            this.Codigo = 0;
            this.Nome = "Não informado";
            this.Preco = 0;
            this.Estoque = 0; // Inicialização automática do estoque
        }

        // Construtor completo
        public ProdutoCadastro(int codigo, string nome, double preco)
        {
            this.Codigo = codigo;
            this.Nome = nome;
            this.Preco = preco;
            this.Estoque = 0; // Estoque sempre inicia automaticamente em 0
        }

        public void ExibirDados()
        {
            Console.WriteLine($"[ProdutoCadastro] Código: {Codigo} | Nome: {Nome} | " +
                $"Preço: {Preco:C} | Estoque: {Estoque}");
        }
    }
}