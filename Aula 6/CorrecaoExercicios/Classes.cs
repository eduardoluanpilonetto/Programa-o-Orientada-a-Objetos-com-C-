using System;
using System.Collections.Generic;

namespace ExerciciosAula6
{
    // =====================================================================
    // 1. ANIMAL E SONS
    // =====================================================================
    public class Animal
    {
        public string Nome { get; set; }

        public Animal(string nome)
        {
            Nome = nome;
        }

        public virtual void EmitirSom()
        {
            Console.WriteLine($"{Nome} emite um som genérico de animal.");
        }
    }

    public class Cachorro : Animal
    {
        public Cachorro(string nome) : base(nome) { }

        public override void EmitirSom()
        {
            Console.WriteLine($"{Nome} (Cachorro) diz: Au au!");
        }
    }

    public class Gato : Animal
    {
        public Gato(string nome) : base(nome) { }

        public override void EmitirSom()
        {
            Console.WriteLine($"{Nome} (Gato) diz: Miau!");
        }
    }

    public class Vaca : Animal
    {
        public Vaca(string nome) : base(nome) { }

        public override void EmitirSom()
        {
            Console.WriteLine($"{Nome} (Vaca) diz: Muuu!");
        }
    }

    // =====================================================================
    // 2. VEÍCULOS
    // =====================================================================
    public class Veiculo
    {
        public string Modelo { get; set; }

        public Veiculo(string modelo)
        {
            Modelo = modelo;
        }

        public virtual void Mover()
        {
            Console.WriteLine($"{Modelo} está se movendo.");
        }
    }

    public class Carro : Veiculo
    {
        public Carro(string modelo) : base(modelo) { }

        public override void Mover()
        {
            Console.WriteLine($"{Modelo}: Carro em movimento nas ruas.");
        }
    }

    public class Moto : Veiculo
    {
        public Moto(string modelo) : base(modelo) { }

        public override void Mover()
        {
            Console.WriteLine($"{Modelo}: Moto acelerando entre os carros.");
        }
    }

    public class Bicicleta : Veiculo
    {
        public Bicicleta(string modelo) : base(modelo) { }

        public override void Mover()
        {
            Console.WriteLine($"{Modelo}: Bicicleta pedalando na ciclovia.");
        }
    }

    // =====================================================================
    // 3. FUNCIONÁRIOS (classe abstrata)
    // =====================================================================
    public abstract class Funcionario
    {
        public string Nome { get; set; }

        public Funcionario(string nome)
        {
            Nome = nome;
        }

        public abstract double CalcularSalario();
    }

    public class Gerente : Funcionario
    {
        public Gerente(string nome) : base(nome) { }

        public override double CalcularSalario()
        {
            return 8000.0;
        }
    }

    public class Programador : Funcionario
    {
        public Programador(string nome) : base(nome) { }

        public override double CalcularSalario()
        {
            return 5000.0;
        }
    }

    public class Estagiario : Funcionario
    {
        public Estagiario(string nome) : base(nome) { }

        public override double CalcularSalario()
        {
            return 1200.0;
        }
    }

    // =====================================================================
    // 4. SISTEMA BANCÁRIO
    // =====================================================================
    public class ContaBancaria
    {
        public string Titular { get; set; }
        public double Saldo { get; protected set; }

        public ContaBancaria(string titular, double saldoInicial)
        {
            Titular = titular;
            Saldo = saldoInicial;
        }

        public virtual void Sacar(double valor)
        {
            if (valor <= Saldo)
            {
                Saldo -= valor;
                Console.WriteLine($"Saque de {valor:C} realizado. Saldo atual: {Saldo:C}");
            }
            else
            {
                Console.WriteLine("Saldo insuficiente.");
            }
        }
    }

    public class ContaCorrente : ContaBancaria
    {
        private double limiteChequeEspecial = 500.0;

        public ContaCorrente(string titular, double saldoInicial) : base(titular, saldoInicial) { }

        public override void Sacar(double valor)
        {
            if (valor <= Saldo + limiteChequeEspecial)
            {
                Saldo -= valor;
                Console.WriteLine($"[Conta Corrente] Saque de {valor:C} realizado (uso de cheque especial, se necessário). Saldo atual: {Saldo:C}");
            }
            else
            {
                Console.WriteLine("[Conta Corrente] Saldo e limite insuficientes.");
            }
        }
    }

    public class ContaPoupanca : ContaBancaria
    {
        public ContaPoupanca(string titular, double saldoInicial) : base(titular, saldoInicial) { }

        public override void Sacar(double valor)
        {
            // Poupança não permite saldo negativo nem cheque especial
            if (valor <= Saldo)
            {
                Saldo -= valor;
                Console.WriteLine($"[Conta Poupança] Saque de {valor:C} realizado. Saldo atual: {Saldo:C}");
            }
            else
            {
                Console.WriteLine("[Conta Poupança] Saldo insuficiente. Poupança não possui limite extra.");
            }
        }
    }

    // =====================================================================
    // 5. SOBRECARGA DE MÉTODOS (Overload)
    // =====================================================================
    public class Calculadora
    {
        public int Somar(int a, int b)
        {
            return a + b;
        }

        public double Somar(double a, double b)
        {
            return a + b;
        }

        public int Somar(int a, int b, int c)
        {
            return a + b + c;
        }
    }

    // =====================================================================
    // 6. SISTEMA DE PAGAMENTOS (interface)
    // =====================================================================
    public interface IPagamento
    {
        void Pagar();
    }

    public class Pix : IPagamento
    {
        public void Pagar()
        {
            Console.WriteLine("Pagamento realizado via Pix.");
        }
    }

    public class CartaoCredito : IPagamento
    {
        public void Pagar()
        {
            Console.WriteLine("Pagamento realizado via Cartão de Crédito.");
        }
    }

    public class Boleto : IPagamento
    {
        public void Pagar()
        {
            Console.WriteLine("Pagamento realizado via Boleto Bancário.");
        }
    }

    // =====================================================================
    // 7. LOJA DE PRODUTOS
    // =====================================================================
    public class Produto
    {
        public string Nome { get; set; }
        public double Preco { get; set; }

        public Produto(string nome, double preco)
        {
            Nome = nome;
            Preco = preco;
        }

        public virtual void ExibirInformacoes()
        {
            Console.WriteLine($"Produto: {Nome} - Preço: {Preco:C}");
        }
    }

    public class Livro : Produto
    {
        public string Autor { get; set; }

        public Livro(string nome, double preco, string autor) : base(nome, preco)
        {
            Autor = autor;
        }

        public override void ExibirInformacoes()
        {
            Console.WriteLine($"[Livro] {Nome} - Autor: {Autor} - Preço: {Preco:C}");
        }
    }

    public class Notebook : Produto
    {
        public string Processador { get; set; }

        public Notebook(string nome, double preco, string processador) : base(nome, preco)
        {
            Processador = processador;
        }

        public override void ExibirInformacoes()
        {
            Console.WriteLine($"[Notebook] {Nome} - Processador: {Processador} - Preço: {Preco:C}");
        }
    }

    public class Celular : Produto
    {
        public string Sistema { get; set; }

        public Celular(string nome, double preco, string sistema) : base(nome, preco)
        {
            Sistema = sistema;
        }

        public override void ExibirInformacoes()
        {
            Console.WriteLine($"[Celular] {Nome} - Sistema: {Sistema} - Preço: {Preco:C}");
        }
    }

    // =====================================================================
    // 8. POLIMORFISMO COM LISTA
    // Reutiliza a classe Animal (exercício 1) — ver Program.cs para o uso
    // com List<Animal> e foreach.
    // =====================================================================

    // =====================================================================
    // 9. SISTEMA ESCOLAR
    // =====================================================================
    public class Pessoa
    {
        public string Nome { get; set; }

        public Pessoa(string nome)
        {
            Nome = nome;
        }

        public virtual void Apresentar()
        {
            Console.WriteLine($"Olá, meu nome é {Nome}.");
        }
    }

    public class Aluno : Pessoa
    {
        public string Turma { get; set; }

        public Aluno(string nome, string turma) : base(nome)
        {
            Turma = turma;
        }

        public override void Apresentar()
        {
            Console.WriteLine($"Sou o aluno {Nome}, da turma {Turma}.");
        }
    }

    public class Professor : Pessoa
    {
        public string Disciplina { get; set; }

        public Professor(string nome, string disciplina) : base(nome)
        {
            Disciplina = disciplina;
        }

        public override void Apresentar()
        {
            Console.WriteLine($"Sou o professor {Nome}, leciono {Disciplina}.");
        }
    }

    public class Coordenador : Pessoa
    {
        public string Area { get; set; }

        public Coordenador(string nome, string area) : base(nome)
        {
            Area = area;
        }

        public override void Apresentar()
        {
            Console.WriteLine($"Sou o coordenador {Nome}, responsável pela área de {Area}.");
        }
    }

    // =====================================================================
    // 10. DESAFIO COMPLETO – RPG
    // =====================================================================
    public class Personagem
    {
        public string Nome { get; set; }

        public Personagem(string nome)
        {
            Nome = nome;
        }

        public virtual void Atacar()
        {
            Console.WriteLine($"{Nome} realiza um ataque básico.");
        }
    }

    public class Guerreiro : Personagem
    {
        public Guerreiro(string nome) : base(nome) { }

        public override void Atacar()
        {
            Console.WriteLine($"{Nome} (Guerreiro) desfere um golpe de espada!");
        }
    }

    public class Mago : Personagem
    {
        public Mago(string nome) : base(nome) { }

        public override void Atacar()
        {
            Console.WriteLine($"{Nome} (Mago) lança uma bola de fogo!");
        }
    }

    public class Arqueiro : Personagem
    {
        public Arqueiro(string nome) : base(nome) { }

        public override void Atacar()
        {
            Console.WriteLine($"{Nome} (Arqueiro) dispara uma flecha certeira!");
        }
    }
}