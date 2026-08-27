using System;
using System.Collections.Generic;

namespace Aula7Abstracao
{
    // ==========================================================
    // Exercício 1 – Classe Abstrata Básica
    // ==========================================================
    public abstract class Animal
    {
        public string Nome { get; set; }

        protected Animal(string nome)
        {
            Nome = nome;
        }

        // Método abstrato: cada animal deve implementar seu próprio som
        public abstract void EmitirSom();
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

    // ==========================================================
    // Exercício 2 – Sistema de Veículos
    // ==========================================================
    public abstract class Veiculo
    {
        public string Modelo { get; set; }

        protected Veiculo(string modelo)
        {
            Modelo = modelo;
        }

        // Método abstrato: cada veículo se move de forma diferente
        public abstract void Mover();

        // Método concreto: comportamento comum a todos os veículos
        public void Abastecer()
        {
            Console.WriteLine($"{Modelo} está sendo abastecido.");
        }
    }

    public class Carro : Veiculo
    {
        public Carro(string modelo) : base(modelo) { }

        public override void Mover()
        {
            Console.WriteLine($"{Modelo} (Carro) se move rodando sobre 4 rodas no asfalto.");
        }
    }

    public class Moto : Veiculo
    {
        public Moto(string modelo) : base(modelo) { }

        public override void Mover()
        {
            Console.WriteLine($"{Modelo} (Moto) se move rápido entre os carros.");
        }
    }

    public class Caminhao : Veiculo
    {
        public Caminhao(string modelo) : base(modelo) { }

        public override void Mover()
        {
            Console.WriteLine($"{Modelo} (Caminhão) se move transportando carga pesada.");
        }
    }

    // ==========================================================
    // Exercício 3 – Interface de Pagamento
    // ==========================================================
    public interface IPagamento
    {
        void Pagar(double valor);
    }

    public class PagamentoPix : IPagamento
    {
        public void Pagar(double valor)
        {
            Console.WriteLine($"Pagamento de R$ {valor:F2} realizado via Pix.");
        }
    }

    public class PagamentoCartao : IPagamento
    {
        public void Pagar(double valor)
        {
            Console.WriteLine($"Pagamento de R$ {valor:F2} realizado via Cartão de Crédito.");
        }
    }

    public class PagamentoBoleto : IPagamento
    {
        public void Pagar(double valor)
        {
            Console.WriteLine($"Pagamento de R$ {valor:F2} gerado via Boleto Bancário.");
        }
    }

    // ==========================================================
    // Exercício 4 – Classe Abstrata + Interface
    // ==========================================================
    public abstract class Funcionario
    {
        public string Nome { get; set; }

        protected Funcionario(string nome)
        {
            Nome = nome;
        }

        public abstract double CalcularSalario();
    }

    public interface IBonificacao
    {
        double CalcularBonus();
    }

    public class Gerente : Funcionario, IBonificacao
    {
        public Gerente(string nome) : base(nome) { }

        public override double CalcularSalario()
        {
            return 8000.0;
        }

        public double CalcularBonus()
        {
            return CalcularSalario() * 0.20; // 20% de bônus
        }
    }

    public class Desenvolvedor : Funcionario, IBonificacao
    {
        public Desenvolvedor(string nome) : base(nome) { }

        public override double CalcularSalario()
        {
            return 6000.0;
        }

        public double CalcularBonus()
        {
            return CalcularSalario() * 0.10; // 10% de bônus
        }
    }

    // ==========================================================
    // Exercício 5 – Sistema de Autenticação
    // ==========================================================
    public interface IAutenticacao
    {
        bool Login(string usuario, string senha);
    }

    public class LoginAdmin : IAutenticacao
    {
        public bool Login(string usuario, string senha)
        {
            bool valido = usuario == "admin" && senha == "admin123";
            Console.WriteLine(valido
                ? "Login de ADMIN realizado com sucesso."
                : "Falha no login de ADMIN.");
            return valido;
        }
    }

    public class LoginCliente : IAutenticacao
    {
        public bool Login(string usuario, string senha)
        {
            bool valido = usuario == "cliente" && senha == "cliente123";
            Console.WriteLine(valido
                ? "Login de CLIENTE realizado com sucesso."
                : "Falha no login de CLIENTE.");
            return valido;
        }
    }

    // ==========================================================
    // Exercício 6 – Interface Segregation Principle (ISP)
    // ==========================================================
    //
    // Interface "ruim" (NÃO deve ser usada, apenas ilustrativa):
    //
    // interface IAnimal
    // {
    //     void Voar();
    //     void Nadar();
    // }
    //
    // Por que ela viola o ISP?
    // O Princípio da Segregação de Interfaces diz que uma classe não deveria
    // ser forçada a implementar métodos que não usa. Se um "Peixe" implementar
    // IAnimal, ele seria obrigado a implementar Voar(), mesmo não voando.
    // O mesmo aconteceria com um "Pássaro" que não sabe nadar. Isso gera
    // métodos vazios ou exceções, quebrando o princípio.
    //
    // Solução: segregar a interface em contratos menores e específicos.

    public interface IVoador
    {
        void Voar();
    }

    public interface INadador
    {
        void Nadar();
    }

    public class Pato : IVoador, INadador
    {
        public void Voar()
        {
            Console.WriteLine("O pato está voando.");
        }

        public void Nadar()
        {
            Console.WriteLine("O pato está nadando.");
        }
    }

    public class Peixe : INadador
    {
        public void Nadar()
        {
            Console.WriteLine("O peixe está nadando.");
        }
    }

    // ==========================================================
    // Exercício 7 – Sistema Bancário
    // ==========================================================
    public abstract class ContaBancaria
    {
        public string Titular { get; set; }
        public double Saldo { get; protected set; }

        protected ContaBancaria(string titular, double saldoInicial)
        {
            Titular = titular;
            Saldo = saldoInicial;
        }

        // Método abstrato: cada tipo de conta tem regra própria de saque
        public abstract void Sacar(double valor);

        // Método concreto: depósito é igual para todas as contas
        public void Depositar(double valor)
        {
            Saldo += valor;
            Console.WriteLine($"{Titular} depositou R$ {valor:F2}. Saldo atual: R$ {Saldo:F2}");
        }
    }

    public class ContaCorrente : ContaBancaria
    {
        public double LimiteChequeEspecial { get; set; }

        public ContaCorrente(string titular, double saldoInicial, double limite)
            : base(titular, saldoInicial)
        {
            LimiteChequeEspecial = limite;
        }

        public override void Sacar(double valor)
        {
            if (valor <= Saldo + LimiteChequeEspecial)
            {
                Saldo -= valor;
                Console.WriteLine($"{Titular} sacou R$ {valor:F2} (Conta Corrente). Saldo atual: R$ {Saldo:F2}");
            }
            else
            {
                Console.WriteLine($"{Titular}: saque negado, valor excede saldo + limite do cheque especial.");
            }
        }
    }

    public class ContaPoupanca : ContaBancaria
    {
        public ContaPoupanca(string titular, double saldoInicial)
            : base(titular, saldoInicial) { }

        public override void Sacar(double valor)
        {
            if (valor <= Saldo)
            {
                Saldo -= valor;
                Console.WriteLine($"{Titular} sacou R$ {valor:F2} (Conta Poupança). Saldo atual: R$ {Saldo:F2}");
            }
            else
            {
                Console.WriteLine($"{Titular}: saque negado, poupança não permite saldo negativo.");
            }
        }
    }

    // ==========================================================
    // Exercício 8 – Polimorfismo com Interfaces
    // ==========================================================
    public interface INotificacao
    {
        void Enviar(string mensagem);
    }

    public class Email : INotificacao
    {
        public void Enviar(string mensagem)
        {
            Console.WriteLine($"[E-mail] Enviando mensagem: \"{mensagem}\"");
        }
    }

    public class SMS : INotificacao
    {
        public void Enviar(string mensagem)
        {
            Console.WriteLine($"[SMS] Enviando mensagem: \"{mensagem}\"");
        }
    }

    public class WhatsApp : INotificacao
    {
        public void Enviar(string mensagem)
        {
            Console.WriteLine($"[WhatsApp] Enviando mensagem: \"{mensagem}\"");
        }
    }

    // ==========================================================
    // Exercício 9 – Sistema de Formas Geométricas
    // ==========================================================
    public interface IForma
    {
        double CalcularArea();
    }

    public class Quadrado : IForma
    {
        public double Lado { get; set; }

        public Quadrado(double lado)
        {
            Lado = lado;
        }

        public double CalcularArea()
        {
            return Lado * Lado;
        }
    }

    public class Retangulo : IForma
    {
        public double Base { get; set; }
        public double Altura { get; set; }

        public Retangulo(double baseR, double altura)
        {
            Base = baseR;
            Altura = altura;
        }

        public double CalcularArea()
        {
            return Base * Altura;
        }
    }

    public class Circulo : IForma
    {
        public double Raio { get; set; }

        public Circulo(double raio)
        {
            Raio = raio;
        }

        public double CalcularArea()
        {
            return Math.PI * Raio * Raio;
        }
    }

    // ==========================================================
    // Exercício 10 – Projeto Completo: Sistema de Transporte
    // ==========================================================
    public interface IManutencao
    {
        void RealizarManutencao();
    }

    public interface INavegacao
    {
        void Navegar();
    }

    public abstract class Transporte
    {
        public string Nome { get; set; }

        protected Transporte(string nome)
        {
            Nome = nome;
        }

        public abstract void ExibirInformacoes();
    }

    public class Aviao : Transporte, IManutencao, INavegacao
    {
        public Aviao(string nome) : base(nome) { }

        public override void ExibirInformacoes()
        {
            Console.WriteLine($"{Nome} é um Avião: transporte aéreo de alta velocidade.");
        }

        public void RealizarManutencao()
        {
            Console.WriteLine($"{Nome}: manutenção de turbinas e sistemas de voo realizada.");
        }

        public void Navegar()
        {
            Console.WriteLine($"{Nome} está navegando pelo espaço aéreo usando radar e GPS.");
        }
    }

    public class Navio : Transporte, IManutencao, INavegacao
    {
        public Navio(string nome) : base(nome) { }

        public override void ExibirInformacoes()
        {
            Console.WriteLine($"{Nome} é um Navio: transporte marítimo de grande capacidade.");
        }

        public void RealizarManutencao()
        {
            Console.WriteLine($"{Nome}: manutenção do casco e motores realizada.");
        }

        public void Navegar()
        {
            Console.WriteLine($"{Nome} está navegando pelo oceano usando bússola e sonar.");
        }
    }

    public class CarroTransporte : Transporte, IManutencao
    {
        public CarroTransporte(string nome) : base(nome) { }

        public override void ExibirInformacoes()
        {
            Console.WriteLine($"{Nome} é um Carro: transporte terrestre individual.");
        }

        public void RealizarManutencao()
        {
            Console.WriteLine($"{Nome}: troca de óleo e revisão dos freios realizada.");
        }

        // Não implementa INavegacao pois um carro comum não "navega" (sem GPS marítimo/aéreo)
    }

    // ==========================================================
    // Desafio Extra – Mini Sistema Escolar
    // (Interfaces + Classe Abstrata + Polimorfismo + Lista de objetos)
    // ==========================================================
    public interface IAvaliavel
    {
        double CalcularMedia();
    }

    public abstract class PessoaEscola
    {
        public string Nome { get; set; }

        protected PessoaEscola(string nome)
        {
            Nome = nome;
        }

        public abstract void Apresentar();
    }

    public class Aluno : PessoaEscola, IAvaliavel
    {
        private List<double> Notas { get; set; }

        public Aluno(string nome, List<double> notas) : base(nome)
        {
            Notas = notas;
        }

        public override void Apresentar()
        {
            Console.WriteLine($"Aluno: {Nome}");
        }

        public double CalcularMedia()
        {
            double soma = 0;
            foreach (double nota in Notas)
                soma += nota;
            return Notas.Count > 0 ? soma / Notas.Count : 0;
        }
    }

    public class Professor : PessoaEscola
    {
        public string Disciplina { get; set; }

        public Professor(string nome, string disciplina) : base(nome)
        {
            Disciplina = disciplina;
        }

        public override void Apresentar()
        {
            Console.WriteLine($"Professor: {Nome} (disciplina: {Disciplina})");
        }
    }
}