using System;
using System.Collections.Generic;

namespace Aula7Abstracao
{
    class Program
    {
        static void Main(string[] args)
        {
            // ==========================================================
            // Exercício 1 – Classe Abstrata Básica
            // ==========================================================
            Console.WriteLine("===== Exercício 1 – Animais =====");
            Animal cachorro = new Cachorro("Rex");
            Animal gato = new Gato("Mimi");
            cachorro.EmitirSom();
            gato.EmitirSom();
            Console.WriteLine();

            // ==========================================================
            // Exercício 2 – Sistema de Veículos
            // ==========================================================
            Console.WriteLine("===== Exercício 2 – Veículos =====");
            Veiculo carro = new Carro("Fusca");
            Veiculo moto = new Moto("CG 160");
            Veiculo caminhao = new Caminhao("Volvo FH");
            carro.Mover();
            moto.Mover();
            caminhao.Mover();
            carro.Abastecer();
            Console.WriteLine();

            // ==========================================================
            // Exercício 3 – Interface de Pagamento
            // ==========================================================
            Console.WriteLine("===== Exercício 3 – Pagamentos =====");
            IPagamento pix = new PagamentoPix();
            IPagamento cartao = new PagamentoCartao();
            IPagamento boleto = new PagamentoBoleto();
            pix.Pagar(150.0);
            cartao.Pagar(300.0);
            boleto.Pagar(89.90);
            Console.WriteLine();

            // ==========================================================
            // Exercício 4 – Classe Abstrata + Interface
            // ==========================================================
            Console.WriteLine("===== Exercício 4 – Funcionários =====");
            Gerente gerente = new Gerente("Carlos");
            Desenvolvedor dev = new Desenvolvedor("Ana");
            Console.WriteLine($"{gerente.Nome} - Salário: R$ {gerente.CalcularSalario():F2} | Bônus: R$ {gerente.CalcularBonus():F2}");
            Console.WriteLine($"{dev.Nome} - Salário: R$ {dev.CalcularSalario():F2} | Bônus: R$ {dev.CalcularBonus():F2}");
            Console.WriteLine();

            // ==========================================================
            // Exercício 5 – Sistema de Autenticação
            // ==========================================================
            Console.WriteLine("===== Exercício 5 – Autenticação =====");
            IAutenticacao loginAdmin = new LoginAdmin();
            IAutenticacao loginCliente = new LoginCliente();
            loginAdmin.Login("admin", "admin123");
            loginAdmin.Login("admin", "senhaErrada");
            loginCliente.Login("cliente", "cliente123");
            Console.WriteLine();

            // ==========================================================
            // Exercício 6 – Interface Segregation Principle
            // ==========================================================
            Console.WriteLine("===== Exercício 6 – ISP (Voador / Nadador) =====");
            Pato pato = new Pato();
            Peixe peixe = new Peixe();
            pato.Voar();
            pato.Nadar();
            peixe.Nadar();
            Console.WriteLine();

            // ==========================================================
            // Exercício 7 – Sistema Bancário
            // ==========================================================
            Console.WriteLine("===== Exercício 7 – Contas Bancárias =====");
            ContaBancaria contaCorrente = new ContaCorrente("João", 100.0, 500.0);
            ContaBancaria contaPoupanca = new ContaPoupanca("Maria", 100.0);
            contaCorrente.Depositar(50.0);
            contaCorrente.Sacar(600.0); // usa o limite do cheque especial
            contaPoupanca.Depositar(50.0);
            contaPoupanca.Sacar(200.0); // deve ser negado
            Console.WriteLine();

            // ==========================================================
            // Exercício 8 – Polimorfismo com Interfaces
            // ==========================================================
            Console.WriteLine("===== Exercício 8 – Notificações =====");
            List<INotificacao> notificacoes = new List<INotificacao>
            {
                new Email(),
                new SMS(),
                new WhatsApp()
            };
            foreach (INotificacao n in notificacoes)
            {
                n.Enviar("Sua encomenda foi enviada!");
            }
            Console.WriteLine();

            // ==========================================================
            // Exercício 9 – Sistema de Formas Geométricas
            // ==========================================================
            Console.WriteLine("===== Exercício 9 – Formas Geométricas =====");
            List<IForma> formas = new List<IForma>
            {
                new Quadrado(4),
                new Retangulo(5, 3),
                new Circulo(2)
            };
            foreach (IForma forma in formas)
            {
                Console.WriteLine($"{forma.GetType().Name}: área = {forma.CalcularArea():F2}");
            }
            Console.WriteLine();

            // ==========================================================
            // Exercício 10 – Projeto Completo: Sistema de Transporte
            // ==========================================================
            Console.WriteLine("===== Exercício 10 – Sistema de Transporte =====");
            List<Transporte> transportes = new List<Transporte>
            {
                new Aviao("Boeing 737"),
                new Navio("Cargueiro Atlântico"),
                new CarroTransporte("Onix")
            };

            foreach (Transporte t in transportes)
            {
                t.ExibirInformacoes();

                if (t is IManutencao manutencao)
                    manutencao.RealizarManutencao();

                if (t is INavegacao navegacao)
                    navegacao.Navegar();

                Console.WriteLine();
            }

            // ==========================================================
            // Desafio Extra – Mini Sistema Escolar
            // ==========================================================
            Console.WriteLine("===== Desafio Extra – Sistema Escolar =====");
            List<PessoaEscola> pessoas = new List<PessoaEscola>
            {
                new Aluno("Pedro", new List<double> { 7.5, 8.0, 6.5 }),
                new Aluno("Julia", new List<double> { 9.0, 9.5, 10.0 }),
                new Professor("Roberto", "Programação Orientada a Objetos")
            };

            foreach (PessoaEscola pessoa in pessoas)
            {
                pessoa.Apresentar();

                if (pessoa is IAvaliavel avaliavel)
                {
                    Console.WriteLine($"   Média final: {avaliavel.CalcularMedia():F2}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Fim da execução. Pressione qualquer tecla para sair.");
            Console.ReadKey();
        }
    }
}