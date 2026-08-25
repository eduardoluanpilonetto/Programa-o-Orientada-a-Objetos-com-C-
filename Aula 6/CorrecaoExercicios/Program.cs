using System;
using System.Collections.Generic;

namespace ExerciciosAula6
{
    class Program
    {
        static void Main(string[] args)
        {
            Exercicio1_AnimalESons();
            Exercicio2_Veiculos();
            Exercicio3_Funcionarios();
            Exercicio4_SistemaBancario();
            Exercicio5_SobrecargaMetodos();
            Exercicio6_SistemaPagamentos();
            Exercicio7_LojaDeProdutos();
            Exercicio8_PolimorfismoComLista();
            Exercicio9_SistemaEscolar();
            Exercicio10_DesafioRPG();
        }

        // =====================================================================
        // 1. ANIMAL E SONS
        // =====================================================================
        static void Exercicio1_AnimalESons()
        {
            Console.WriteLine("===== 1. Animal e Sons =====");

            Animal cachorro = new Cachorro("Rex");
            Animal gato = new Gato("Felix");
            Animal vaca = new Vaca("Mimosa");

            cachorro.EmitirSom();
            gato.EmitirSom();
            vaca.EmitirSom();

            Console.WriteLine();
        }

        // =====================================================================
        // 2. VEÍCULOS
        // =====================================================================
        static void Exercicio2_Veiculos()
        {
            Console.WriteLine("===== 2. Veículos =====");

            Veiculo carro = new Carro("Fusca");
            Veiculo moto = new Moto("CG 160");
            Veiculo bicicleta = new Bicicleta("Caloi 10");

            carro.Mover();
            moto.Mover();
            bicicleta.Mover();

            Console.WriteLine();
        }

        // =====================================================================
        // 3. FUNCIONÁRIOS
        // =====================================================================
        static void Exercicio3_Funcionarios()
        {
            Console.WriteLine("===== 3. Funcionários =====");

            Funcionario gerente = new Gerente("Ana");
            Funcionario programador = new Programador("Bruno");
            Funcionario estagiario = new Estagiario("Carla");

            Console.WriteLine($"{gerente.Nome} (Gerente) - Salário: {gerente.CalcularSalario():C}");
            Console.WriteLine($"{programador.Nome} (Programador) - Salário: {programador.CalcularSalario():C}");
            Console.WriteLine($"{estagiario.Nome} (Estagiário) - Salário: {estagiario.CalcularSalario():C}");

            Console.WriteLine();
        }

        // =====================================================================
        // 4. SISTEMA BANCÁRIO
        // =====================================================================
        static void Exercicio4_SistemaBancario()
        {
            Console.WriteLine("===== 4. Sistema Bancário =====");

            ContaBancaria contaCorrente = new ContaCorrente("João", 1000.0);
            ContaBancaria contaPoupanca = new ContaPoupanca("Maria", 500.0);

            contaCorrente.Sacar(1300.0); // usa o cheque especial
            contaPoupanca.Sacar(600.0);  // saldo insuficiente, sem limite extra

            Console.WriteLine();
        }

        // =====================================================================
        // 5. SOBRECARGA DE MÉTODOS
        // =====================================================================
        static void Exercicio5_SobrecargaMetodos()
        {
            Console.WriteLine("===== 5. Sobrecarga de Métodos =====");

            Calculadora calc = new Calculadora();

            Console.WriteLine($"Somar(2, 3) = {calc.Somar(2, 3)}");
            Console.WriteLine($"Somar(2.5, 3.7) = {calc.Somar(2.5, 3.7)}");
            Console.WriteLine($"Somar(1, 2, 3) = {calc.Somar(1, 2, 3)}");

            Console.WriteLine();
        }

        // =====================================================================
        // 6. SISTEMA DE PAGAMENTOS
        // =====================================================================
        static void Exercicio6_SistemaPagamentos()
        {
            Console.WriteLine("===== 6. Sistema de Pagamentos =====");

            List<IPagamento> pagamentos = new List<IPagamento>
            {
                new Pix(),
                new CartaoCredito(),
                new Boleto()
            };

            foreach (IPagamento pagamento in pagamentos)
            {
                pagamento.Pagar();
            }

            Console.WriteLine();
        }

        // =====================================================================
        // 7. LOJA DE PRODUTOS
        // =====================================================================
        static void Exercicio7_LojaDeProdutos()
        {
            Console.WriteLine("===== 7. Loja de Produtos =====");

            List<Produto> produtos = new List<Produto>
            {
                new Livro("Dom Casmurro", 39.90, "Machado de Assis"),
                new Notebook("Dell Inspiron", 3500.00, "Intel i7"),
                new Celular("iPhone 14", 5999.00, "iOS")
            };

            foreach (Produto produto in produtos)
            {
                produto.ExibirInformacoes();
            }

            Console.WriteLine();
        }

        // =====================================================================
        // 8. POLIMORFISMO COM LISTA
        // =====================================================================
        static void Exercicio8_PolimorfismoComLista()
        {
            Console.WriteLine("===== 8. Polimorfismo com Lista =====");

            List<Animal> animais = new List<Animal>
            {
                new Cachorro("Bidu"),
                new Gato("Tom"),
                new Vaca("Mel")
            };

            foreach (Animal animal in animais)
            {
                animal.EmitirSom(); // chamada polimórfica
            }

            Console.WriteLine();
        }

        // =====================================================================
        // 9. SISTEMA ESCOLAR
        // =====================================================================
        static void Exercicio9_SistemaEscolar()
        {
            Console.WriteLine("===== 9. Sistema Escolar =====");

            List<Pessoa> pessoas = new List<Pessoa>
            {
                new Aluno("Lucas", "3º Ano A"),
                new Professor("Fernanda", "Matemática"),
                new Coordenador("Ricardo", "Ensino Médio")
            };

            foreach (Pessoa pessoa in pessoas)
            {
                pessoa.Apresentar();
            }

            Console.WriteLine();
        }

        // =====================================================================
        // 10. DESAFIO COMPLETO – RPG
        // =====================================================================
        static void Exercicio10_DesafioRPG()
        {
            Console.WriteLine("===== 10. Desafio Completo - RPG =====");

            List<Personagem> personagens = new List<Personagem>
            {
                new Guerreiro("Conan"),
                new Mago("Merlin"),
                new Arqueiro("Legolas")
            };

            // Desafio extra: percorre a lista executando os ataques polimorficamente
            foreach (Personagem personagem in personagens)
            {
                personagem.Atacar();
            }

            Console.WriteLine();
        }
    }
}