// ============================================================================
// AULA 10 - TRATAMENTO DE EXCEÇÕES EM C#
// Correção da lista de exercícios (10 exercícios + desafio extra)
// Compatível com .NET 6 ou superior (dotnet new console)
// ============================================================================

using System;
using System.Collections.Generic;
using System.Globalization;

namespace Aula10Excecoes
{
    // ------------------------------------------------------------------------
    // EXCEÇÕES PERSONALIZADAS
    // Regra: herdar de Exception e chamar o construtor base com a mensagem.
    // ------------------------------------------------------------------------

    // Exercício 6 (e Desafio Extra)
    public class SaldoInsuficienteException : Exception
    {
        public decimal SaldoAtual { get; }
        public decimal ValorSolicitado { get; }

        public SaldoInsuficienteException(decimal saldoAtual, decimal valorSolicitado)
            : base($"Saldo insuficiente. Saldo atual: {saldoAtual:C}, valor solicitado: {valorSolicitado:C}.")
        {
            SaldoAtual = saldoAtual;
            ValorSolicitado = valorSolicitado;
        }
    }

    // Exercício 7
    public class CredenciaisInvalidasException : Exception
    {
        public CredenciaisInvalidasException()
            : base("Usuário ou senha incorretos.") { }
    }

    // Exercício 10
    public class ProdutoInvalidoException : Exception
    {
        public ProdutoInvalidoException(string mensagem) : base(mensagem) { }
    }

    // Desafio Extra
    public class ValorInvalidoException : Exception
    {
        public ValorInvalidoException(string mensagem) : base(mensagem) { }
    }

    public class ContaNaoEncontradaException : Exception
    {
        public ContaNaoEncontradaException(string numeroConta)
            : base($"Conta '{numeroConta}' não encontrada.") { }
    }

    // ------------------------------------------------------------------------
    // PROGRAMA PRINCIPAL (menu)
    // ------------------------------------------------------------------------
    internal class Program
    {
        static void Main(string[] args)
        {
            bool sair = false;

            while (!sair)
            {
                Console.WriteLine("\n==================== AULA 10 - EXCEÇÕES ====================");
                Console.WriteLine(" 1  - Divisão segura");
                Console.WriteLine(" 2  - Conversão de dados (idade)");
                Console.WriteLine(" 3  - Uso do finally");
                Console.WriteLine(" 4  - Múltiplos catch");
                Console.WriteLine(" 5  - Cadastro de usuário");
                Console.WriteLine(" 6  - Exceção personalizada (saque)");
                Console.WriteLine(" 7  - Sistema de login");
                Console.WriteLine(" 8  - Calculadora robusta");
                Console.WriteLine(" 9  - Validação com TryParse");
                Console.WriteLine(" 10 - Cadastro de produtos");
                Console.WriteLine(" 11 - Desafio extra: sistema bancário");
                Console.WriteLine(" 0  - Sair");
                Console.Write("Escolha: ");

                string opcao = Console.ReadLine() ?? "";

                switch (opcao.Trim())
                {
                    case "1": Exercicio1(); break;
                    case "2": Exercicio2(); break;
                    case "3": Exercicio3(); break;
                    case "4": Exercicio4(); break;
                    case "5": Exercicio5(); break;
                    case "6": Exercicio6(); break;
                    case "7": Exercicio7(); break;
                    case "8": Exercicio8(); break;
                    case "9": Exercicio9(); break;
                    case "10": Exercicio10(); break;
                    case "11": DesafioExtra(); break;
                    case "0": sair = true; break;
                    default: Console.WriteLine("Opção inválida."); break;
                }
            }

            Console.WriteLine("Programa encerrado.");
        }

        // ====================================================================
        // EXERCÍCIO 1 - Divisão Segura
        // Conceito: try/catch + DivideByZeroException.
        // Obs.: divisão de int por zero lança exceção; divisão de double por
        // zero NÃO lança (retorna Infinity). Por isso usamos int/decimal aqui.
        // ====================================================================
        static void Exercicio1()
        {
            Console.WriteLine("\n--- Exercício 1: Divisão Segura ---");

            try
            {
                Console.Write("Digite o primeiro número (inteiro): ");
                int a = int.Parse(Console.ReadLine() ?? "");

                Console.Write("Digite o segundo número (inteiro): ");
                int b = int.Parse(Console.ReadLine() ?? "");

                int resultado = a / b; // lança DivideByZeroException se b == 0
                Console.WriteLine($"Resultado: {a} / {b} = {resultado}");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Não é possível dividir por zero. Tente outro divisor.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Entrada inválida. Digite apenas números inteiros.");
            }
        }

        // ====================================================================
        // EXERCÍCIO 2 - Conversão de Dados
        // Conceito: FormatException + laço para permitir nova tentativa.
        // ====================================================================
        static void Exercicio2()
        {
            Console.WriteLine("\n--- Exercício 2: Conversão de Dados ---");

            bool valido = false;

            while (!valido)
            {
                try
                {
                    Console.Write("Digite sua idade: ");
                    int idade = int.Parse(Console.ReadLine() ?? "");

                    Console.WriteLine($"Você tem {idade} anos.");
                    valido = true; // só chega aqui se o Parse funcionou
                }
                catch (FormatException)
                {
                    Console.WriteLine("Formato inválido! Digite apenas números. Tente novamente.\n");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Número grande demais! Tente novamente.\n");
                }
            }
        }

        // ====================================================================
        // EXERCÍCIO 3 - Uso do finally
        // Conceito: finally SEMPRE executa (com ou sem erro).
        // ====================================================================
        static void Exercicio3()
        {
            Console.WriteLine("\n--- Exercício 3: Uso do finally ---");

            try
            {
                Console.WriteLine("Abrindo o arquivo...");

                // Simulando um erro durante a leitura
                throw new System.IO.IOException("Falha simulada ao ler o arquivo.");
            }
            catch (System.IO.IOException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            {
                // Ideal para liberar recursos (arquivos, conexões, etc.)
                Console.WriteLine("Arquivo fechado com sucesso");
            }
        }

        // ====================================================================
        // EXERCÍCIO 4 - Múltiplos catch
        // Regra: do mais ESPECÍFICO para o mais GENÉRICO (Exception por último).
        // ====================================================================
        static void Exercicio4()
        {
            Console.WriteLine("\n--- Exercício 4: Múltiplos catch ---");

            try
            {
                Console.Write("Digite o primeiro número: ");
                int a = int.Parse(Console.ReadLine() ?? "");

                Console.Write("Digite o segundo número: ");
                int b = int.Parse(Console.ReadLine() ?? "");

                Console.WriteLine($"Resultado: {a / b}");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("[Erro de divisão] Não é permitido dividir por zero.");
            }
            catch (FormatException)
            {
                Console.WriteLine("[Erro de formato] Você digitou um valor que não é um número.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Erro inesperado] {ex.Message}");
            }
        }

        // ====================================================================
        // EXERCÍCIO 5 - Cadastro de Usuário
        // Conceito: validar dados e lançar exceção quando a regra for violada.
        // ====================================================================
        static void Exercicio5()
        {
            Console.WriteLine("\n--- Exercício 5: Cadastro de Usuário ---");

            try
            {
                Console.Write("Nome: ");
                string nome = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(nome))
                    throw new ArgumentException("O nome não pode ser vazio.");

                Console.Write("Idade: ");
                int idade = int.Parse(Console.ReadLine() ?? ""); // FormatException se não numérica

                if (idade < 0)
                    throw new ArgumentOutOfRangeException(nameof(idade), "A idade não pode ser negativa.");

                Console.WriteLine($"Usuário cadastrado com sucesso: {nome}, {idade} anos.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Erro: a idade deve ser um número inteiro.");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Erro: a idade não pode ser negativa.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        // ====================================================================
        // EXERCÍCIO 6 - Exceção Personalizada (SaldoInsuficienteException)
        // A classe da exceção está no topo do arquivo.
        // ====================================================================
        static void Exercicio6()
        {
            Console.WriteLine("\n--- Exercício 6: Exceção Personalizada ---");

            decimal saldo = 500m;
            Console.WriteLine($"Saldo atual: {saldo:C}");

            try
            {
                Console.Write("Valor do saque: ");
                decimal valor = decimal.Parse(Console.ReadLine() ?? "");

                if (valor > saldo)
                    throw new SaldoInsuficienteException(saldo, valor);

                saldo -= valor;
                Console.WriteLine($"Saque realizado! Novo saldo: {saldo:C}");
            }
            catch (SaldoInsuficienteException ex)
            {
                Console.WriteLine($"Operação negada: {ex.Message}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Valor inválido. Digite um número.");
            }
        }

        // ====================================================================
        // EXERCÍCIO 7 - Sistema de Login
        // ====================================================================
        static void Exercicio7()
        {
            Console.WriteLine("\n--- Exercício 7: Sistema de Login ---");

            try
            {
                Console.Write("Usuário: ");
                string usuario = Console.ReadLine() ?? "";

                Console.Write("Senha: ");
                string senha = Console.ReadLine() ?? "";

                if (usuario != "admin" || senha != "1234")
                    throw new CredenciaisInvalidasException();

                Console.WriteLine("Login realizado com sucesso! Bem-vindo, admin.");
            }
            catch (CredenciaisInvalidasException ex)
            {
                Console.WriteLine($"Erro de autenticação: {ex.Message}");
            }
        }

        // ====================================================================
        // EXERCÍCIO 8 - Calculadora Robusta
        // Conceito: try/catch/finally dentro de um laço que nunca "quebra".
        // ====================================================================
        static void Exercicio8()
        {
            Console.WriteLine("\n--- Exercício 8: Calculadora Robusta ---");

            bool continuar = true;

            while (continuar)
            {
                try
                {
                    Console.WriteLine("\n1-Soma  2-Subtração  3-Multiplicação  4-Divisão  0-Voltar");
                    Console.Write("Operação: ");
                    string op = Console.ReadLine() ?? "";

                    if (op == "0")
                    {
                        continuar = false;
                        continue; // o finally ainda será executado
                    }

                    if (op != "1" && op != "2" && op != "3" && op != "4")
                        throw new ArgumentException("Operação inválida. Escolha de 0 a 4.");

                    Console.Write("Primeiro número: ");
                    decimal a = decimal.Parse(Console.ReadLine() ?? "");

                    Console.Write("Segundo número: ");
                    decimal b = decimal.Parse(Console.ReadLine() ?? "");

                    decimal resultado = op switch
                    {
                        "1" => a + b,
                        "2" => a - b,
                        "3" => a * b,
                        _ => a / b // decimal lança DivideByZeroException
                    };

                    Console.WriteLine($"Resultado: {resultado}");
                }
                catch (DivideByZeroException)
                {
                    Console.WriteLine("Erro: divisão por zero não é permitida.");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Erro: digite apenas números válidos.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Erro: o número é grande demais.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
                finally
                {
                    Console.WriteLine("(fim da operação)");
                }
            }
        }

        // ====================================================================
        // EXERCÍCIO 9 - Validação com TryParse
        // TryParse retorna bool (sem lançar exceção). É mais performático e
        // mais indicado para validação simples de entrada do usuário.
        // ====================================================================
        static void Exercicio9()
        {
            Console.WriteLine("\n--- Exercício 9: Validação com TryParse ---");

            Console.Write("Digite um número inteiro: ");
            string entrada = Console.ReadLine() ?? "";

            if (int.TryParse(entrada, out int numero))
            {
                Console.WriteLine($"Valor válido: {numero}");
            }
            else
            {
                Console.WriteLine($"Valor inválido: \"{entrada}\" não é um número inteiro.");
            }
        }

        // ====================================================================
        // EXERCÍCIO 10 - Sistema Completo (cadastro de produtos)
        // ====================================================================
        static void Exercicio10()
        {
            Console.WriteLine("\n--- Exercício 10: Cadastro de Produtos ---");

            var produtos = new List<(string Nome, decimal Preco, int Quantidade)>();
            string resposta;

            do
            {
                try
                {
                    Console.Write("Nome do produto: ");
                    string nome = Console.ReadLine() ?? "";

                    if (string.IsNullOrWhiteSpace(nome))
                        throw new ProdutoInvalidoException("O nome do produto não pode ser vazio.");

                    Console.Write("Preço: ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal preco))
                        throw new ProdutoInvalidoException("O preço informado não é um número válido.");

                    if (preco <= 0)
                        throw new ProdutoInvalidoException("O preço deve ser maior que zero.");

                    Console.Write("Quantidade: ");
                    if (!int.TryParse(Console.ReadLine(), out int quantidade))
                        throw new ProdutoInvalidoException("A quantidade informada não é um número inteiro válido.");

                    if (quantidade < 0)
                        throw new ProdutoInvalidoException("A quantidade não pode ser negativa.");

                    produtos.Add((nome, preco, quantidade));
                    Console.WriteLine("Produto cadastrado com sucesso!");
                }
                catch (ProdutoInvalidoException ex)
                {
                    // "Registrar mensagens de erro no console"
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[ERRO] {DateTime.Now:HH:mm:ss} - {ex.Message}");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[ERRO INESPERADO] {DateTime.Now:HH:mm:ss} - {ex.Message}");
                    Console.ResetColor();
                }
                finally
                {
                    Console.WriteLine($"Total de produtos cadastrados até agora: {produtos.Count}");
                }

                Console.Write("Deseja cadastrar outro produto? (s/n): ");
                resposta = (Console.ReadLine() ?? "").Trim().ToLower();

            } while (resposta == "s");

            Console.WriteLine("\n--- Produtos cadastrados ---");
            foreach (var p in produtos)
            {
                Console.WriteLine($"{p.Nome} | {p.Preco:C} | Qtd: {p.Quantidade}");
            }
        }

        // ====================================================================
        // DESAFIO EXTRA - Sistema Bancário
        // Depósito, saque e transferência, com exceções personalizadas,
        // logs das operações e um programa que nunca encerra inesperadamente.
        // ====================================================================
        static void DesafioExtra()
        {
            Console.WriteLine("\n--- Desafio Extra: Sistema Bancário ---");

            var banco = new Banco();
            banco.CriarConta("001", "Ana", 1000m);
            banco.CriarConta("002", "Bruno", 500m);

            bool continuar = true;

            while (continuar)
            {
                try
                {
                    Console.WriteLine("\n1-Depósito  2-Saque  3-Transferência  4-Ver saldos  5-Ver logs  0-Voltar");
                    Console.Write("Opção: ");
                    string op = Console.ReadLine() ?? "";

                    switch (op)
                    {
                        case "1":
                            Console.Write("Conta: ");
                            string contaDep = Console.ReadLine() ?? "";
                            banco.Depositar(contaDep, LerDecimal("Valor do depósito: "));
                            break;

                        case "2":
                            Console.Write("Conta: ");
                            string contaSaq = Console.ReadLine() ?? "";
                            banco.Sacar(contaSaq, LerDecimal("Valor do saque: "));
                            break;

                        case "3":
                            Console.Write("Conta de origem: ");
                            string origem = Console.ReadLine() ?? "";
                            Console.Write("Conta de destino: ");
                            string destino = Console.ReadLine() ?? "";
                            banco.Transferir(origem, destino, LerDecimal("Valor da transferência: "));
                            break;

                        case "4":
                            banco.ExibirSaldos();
                            break;

                        case "5":
                            banco.ExibirLogs();
                            break;

                        case "0":
                            continuar = false;
                            break;

                        default:
                            Console.WriteLine("Opção inválida.");
                            break;
                    }
                }
                catch (SaldoInsuficienteException ex)
                {
                    banco.Log($"FALHA - {ex.Message}");
                }
                catch (ContaNaoEncontradaException ex)
                {
                    banco.Log($"FALHA - {ex.Message}");
                }
                catch (ValorInvalidoException ex)
                {
                    banco.Log($"FALHA - {ex.Message}");
                }
                catch (FormatException)
                {
                    banco.Log("FALHA - valor digitado não é um número válido.");
                }
                catch (Exception ex)
                {
                    // Rede de segurança: nada derruba o sistema
                    banco.Log($"ERRO INESPERADO - {ex.Message}");
                }
            }
        }

        // Lê um decimal; FormatException sobe para ser tratada por quem chamou
        static decimal LerDecimal(string mensagem)
        {
            Console.Write(mensagem);
            return decimal.Parse(Console.ReadLine() ?? "", CultureInfo.CurrentCulture);
        }
    }

    // ------------------------------------------------------------------------
    // CLASSES DO DESAFIO EXTRA
    // ------------------------------------------------------------------------
    public class ContaBancaria
    {
        public string Numero { get; }
        public string Titular { get; }
        public decimal Saldo { get; private set; }

        public ContaBancaria(string numero, string titular, decimal saldoInicial)
        {
            Numero = numero;
            Titular = titular;
            Saldo = saldoInicial;
        }

        public void Depositar(decimal valor)
        {
            if (valor <= 0)
                throw new ValorInvalidoException("O valor do depósito deve ser maior que zero.");

            Saldo += valor;
        }

        public void Sacar(decimal valor)
        {
            if (valor <= 0)
                throw new ValorInvalidoException("O valor do saque deve ser maior que zero.");

            if (valor > Saldo)
                throw new SaldoInsuficienteException(Saldo, valor);

            Saldo -= valor;
        }
    }

    public class Banco
    {
        private readonly Dictionary<string, ContaBancaria> _contas = new();
        private readonly List<string> _logs = new();

        public void CriarConta(string numero, string titular, decimal saldoInicial)
        {
            _contas[numero] = new ContaBancaria(numero, titular, saldoInicial);
        }

        private ContaBancaria BuscarConta(string numero)
        {
            if (!_contas.TryGetValue(numero.Trim(), out ContaBancaria? conta))
                throw new ContaNaoEncontradaException(numero);

            return conta;
        }

        public void Depositar(string numeroConta, decimal valor)
        {
            try
            {
                var conta = BuscarConta(numeroConta);
                conta.Depositar(valor);
                Log($"SUCESSO - Depósito de {valor:C} na conta {conta.Numero} ({conta.Titular}).");
            }
            finally
            {
                // O finally garante que a tentativa fique registrada
                _logs.Add($"[{DateTime.Now:HH:mm:ss}] Tentativa de depósito na conta {numeroConta}.");
            }
        }

        public void Sacar(string numeroConta, decimal valor)
        {
            try
            {
                var conta = BuscarConta(numeroConta);
                conta.Sacar(valor);
                Log($"SUCESSO - Saque de {valor:C} na conta {conta.Numero} ({conta.Titular}).");
            }
            finally
            {
                _logs.Add($"[{DateTime.Now:HH:mm:ss}] Tentativa de saque na conta {numeroConta}.");
            }
        }

        public void Transferir(string origem, string destino, decimal valor)
        {
            try
            {
                var contaOrigem = BuscarConta(origem);
                var contaDestino = BuscarConta(destino);

                if (contaOrigem.Numero == contaDestino.Numero)
                    throw new ValorInvalidoException("A conta de origem e a de destino devem ser diferentes.");

                // Sacar valida o valor e o saldo ANTES de depositar,
                // então não há risco de "perder" dinheiro no meio da operação.
                contaOrigem.Sacar(valor);
                contaDestino.Depositar(valor);

                Log($"SUCESSO - Transferência de {valor:C} de {contaOrigem.Numero} para {contaDestino.Numero}.");
            }
            finally
            {
                _logs.Add($"[{DateTime.Now:HH:mm:ss}] Tentativa de transferência de {origem} para {destino}.");
            }
        }

        public void ExibirSaldos()
        {
            foreach (var c in _contas.Values)
                Console.WriteLine($"Conta {c.Numero} - {c.Titular}: {c.Saldo:C}");
        }

        public void ExibirLogs()
        {
            Console.WriteLine("\n--- LOGS DAS OPERAÇÕES ---");
            if (_logs.Count == 0)
            {
                Console.WriteLine("Nenhuma operação registrada.");
                return;
            }

            foreach (var l in _logs)
                Console.WriteLine(l);
        }

        public void Log(string mensagem)
        {
            string linha = $"[{DateTime.Now:HH:mm:ss}] {mensagem}";
            _logs.Add(linha);

            if (mensagem.StartsWith("SUCESSO"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.WriteLine(linha);
            Console.ResetColor();
        }
    }
}