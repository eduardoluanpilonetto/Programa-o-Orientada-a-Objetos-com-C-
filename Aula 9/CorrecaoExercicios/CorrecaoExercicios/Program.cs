using System;
using System.Collections.Generic;
using System.Linq;

namespace ExerciciosAula9
{
    // ===================== Classe usada no Exercício 5 =====================
    class Produto
    {
        public string Nome { get; set; }
        public double Preco { get; set; }

        public override string ToString()
        {
            return $"Produto: {Nome} | Preço: R$ {Preco:F2}";
        }
    }

    // ===================== Classe usada no Exercício 7 =====================
    class Aluno
    {
        public string Nome { get; set; }
        public double Nota { get; set; }

        public override string ToString()
        {
            return $"Aluno: {Nome} | Nota: {Nota}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int opcao;

            do
            {
                Console.Clear();
                Console.WriteLine("===== EXERCÍCIOS AULA 9 - Arrays e Coleções de Objetos =====");
                Console.WriteLine("1  - Array de Números");
                Console.WriteLine("2  - Soma de Valores do Array");
                Console.WriteLine("3  - Média de Notas");
                Console.WriteLine("4  - Lista de Nomes");
                Console.WriteLine("5  - Cadastro de Produtos");
                Console.WriteLine("6  - Removendo Itens da Lista");
                Console.WriteLine("7  - Busca em Lista");
                Console.WriteLine("8  - Dictionary de Alunos");
                Console.WriteLine("9  - Simulação de Fila");
                Console.WriteLine("10 - Controle de Navegação");
                Console.WriteLine("0  - Sair");
                Console.Write("\nEscolha o exercício: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Opção inválida!");
                    Pausar();
                    continue;
                }

                Console.Clear();

                switch (opcao)
                {
                    case 1: Exercicio1(); break;
                    case 2: Exercicio2(); break;
                    case 3: Exercicio3(); break;
                    case 4: Exercicio4(); break;
                    case 5: Exercicio5(); break;
                    case 6: Exercicio6(); break;
                    case 7: Exercicio7(); break;
                    case 8: Exercicio8(); break;
                    case 9: Exercicio9(); break;
                    case 10: Exercicio10(); break;
                    case 0: Console.WriteLine("Encerrando..."); break;
                    default: Console.WriteLine("Opção inválida!"); break;
                }

                if (opcao != 0) Pausar();

            } while (opcao != 0);
        }

        static void Pausar()
        {
            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
        }

        // ===================== Exercício 1 – Array de Números =====================
        static void Exercicio1()
        {
            Console.WriteLine("--- Exercício 1: Array de Números ---\n");

            int[] numeros = new int[10];

            for (int i = 0; i < numeros.Length; i++)
            {
                Console.Write($"Digite o {i + 1}º número: ");
                numeros[i] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine("\nNúmeros cadastrados:");
            foreach (int numero in numeros)
            {
                Console.WriteLine(numero);
            }
        }

        // ===================== Exercício 2 – Soma de Valores do Array =====================
        static void Exercicio2()
        {
            Console.WriteLine("--- Exercício 2: Soma de Valores do Array ---\n");

            int[] numeros = new int[5];
            int soma = 0;

            for (int i = 0; i < numeros.Length; i++)
            {
                Console.Write($"Digite o {i + 1}º número: ");
                numeros[i] = int.Parse(Console.ReadLine());
                soma += numeros[i];
            }

            Console.WriteLine($"\nA soma total dos valores é: {soma}");
        }

        // ===================== Exercício 3 – Média de Notas =====================
        static void Exercicio3()
        {
            Console.WriteLine("--- Exercício 3: Média de Notas ---\n");

            double[] notas = new double[4];
            double soma = 0;

            for (int i = 0; i < notas.Length; i++)
            {
                Console.Write($"Digite a {i + 1}ª nota: ");
                notas[i] = double.Parse(Console.ReadLine());
                soma += notas[i];
            }

            double media = soma / notas.Length;

            Console.WriteLine($"\nMédia do aluno: {media:F2}");
            Console.WriteLine(media >= 6 ? "Situação: APROVADO" : "Situação: REPROVADO");
        }

        // ===================== Exercício 4 – Lista de Nomes =====================
        static void Exercicio4()
        {
            Console.WriteLine("--- Exercício 4: Lista de Nomes ---\n");

            List<string> nomes = new List<string>();

            for (int i = 0; i < 5; i++)
            {
                Console.Write($"Digite o {i + 1}º nome: ");
                nomes.Add(Console.ReadLine());
            }

            Console.WriteLine("\nNomes cadastrados:");
            foreach (string nome in nomes)
            {
                Console.WriteLine(nome);
            }
        }

        // ===================== Exercício 5 – Cadastro de Produtos =====================
        static void Exercicio5()
        {
            Console.WriteLine("--- Exercício 5: Cadastro de Produtos ---\n");

            List<Produto> produtos = new List<Produto>();
            int quantidade = 3; // quantidade de produtos a cadastrar

            for (int i = 0; i < quantidade; i++)
            {
                Console.WriteLine($"\nProduto {i + 1}:");
                Console.Write("Nome: ");
                string nome = Console.ReadLine();
                Console.Write("Preço: ");
                double preco = double.Parse(Console.ReadLine());

                produtos.Add(new Produto { Nome = nome, Preco = preco });
            }

            Console.WriteLine("\nProdutos cadastrados:");
            foreach (Produto produto in produtos)
            {
                Console.WriteLine(produto);
            }
        }

        // ===================== Exercício 6 – Removendo Itens da Lista =====================
        static void Exercicio6()
        {
            Console.WriteLine("--- Exercício 6: Removendo Itens da Lista ---\n");

            List<string> cidades = new List<string>
            {
                "São Paulo", "Rio de Janeiro", "Curitiba", "Florianópolis", "Porto Alegre"
            };

            Console.WriteLine("Lista original:");
            cidades.ForEach(c => Console.WriteLine(c));

            Console.Write("\nDigite o nome da cidade a ser removida: ");
            string cidadeRemover = Console.ReadLine();

            if (cidades.Remove(cidadeRemover))
            {
                Console.WriteLine($"\nCidade '{cidadeRemover}' removida com sucesso!");
            }
            else
            {
                Console.WriteLine("\nCidade não encontrada na lista.");
            }

            Console.WriteLine("\nLista atualizada:");
            cidades.ForEach(c => Console.WriteLine(c));
        }

        // ===================== Exercício 7 – Busca em Lista =====================
        static void Exercicio7()
        {
            Console.WriteLine("--- Exercício 7: Busca em Lista ---\n");

            List<Aluno> alunos = new List<Aluno>
            {
                new Aluno { Nome = "João", Nota = 8.5 },
                new Aluno { Nome = "Maria", Nota = 9.2 },
                new Aluno { Nome = "Pedro", Nota = 6.0 },
                new Aluno { Nome = "Ana", Nota = 7.8 }
            };

            Console.Write("Digite o nome do aluno que deseja pesquisar: ");
            string nomePesquisado = Console.ReadLine();

            Aluno encontrado = alunos.Find(a =>
                a.Nome.Equals(nomePesquisado, StringComparison.OrdinalIgnoreCase));

            if (encontrado != null)
            {
                Console.WriteLine("\nAluno encontrado:");
                Console.WriteLine(encontrado);
            }
            else
            {
                Console.WriteLine("\nAluno não encontrado.");
            }
        }

        // ===================== Exercício 8 – Dictionary de Alunos =====================
        static void Exercicio8()
        {
            Console.WriteLine("--- Exercício 8: Dictionary de Alunos ---\n");

            Dictionary<int, string> alunos = new Dictionary<int, string>();
            int quantidade = 3;

            for (int i = 0; i < quantidade; i++)
            {
                Console.Write($"\nDigite a matrícula do {i + 1}º aluno: ");
                int matricula = int.Parse(Console.ReadLine());
                Console.Write("Digite o nome do aluno: ");
                string nome = Console.ReadLine();

                alunos[matricula] = nome;
            }

            Console.WriteLine("\nAlunos cadastrados:");
            foreach (KeyValuePair<int, string> item in alunos)
            {
                Console.WriteLine($"Matrícula: {item.Key} - Nome: {item.Value}");
            }
        }

        // ===================== Exercício 9 – Simulação de Fila =====================
        static void Exercicio9()
        {
            Console.WriteLine("--- Exercício 9: Simulação de Fila ---\n");

            Queue<string> filaAtendimento = new Queue<string>();

            filaAtendimento.Enqueue("Carlos");
            filaAtendimento.Enqueue("Beatriz");
            filaAtendimento.Enqueue("Ricardo");
            filaAtendimento.Enqueue("Fernanda");

            Console.WriteLine("Fila de atendimento:");
            foreach (string pessoa in filaAtendimento)
            {
                Console.WriteLine(pessoa);
            }

            Console.WriteLine("\nIniciando atendimento...\n");

            string atendido1 = filaAtendimento.Dequeue();
            Console.WriteLine($"Atendendo: {atendido1}");

            string atendido2 = filaAtendimento.Dequeue();
            Console.WriteLine($"Atendendo: {atendido2}");

            Console.WriteLine("\nFila restante:");
            foreach (string pessoa in filaAtendimento)
            {
                Console.WriteLine(pessoa);
            }
        }

        // ===================== Exercício 10 – Controle de Navegação =====================
        static void Exercicio10()
        {
            Console.WriteLine("--- Exercício 10: Controle de Navegação ---\n");

            Stack<string> paginasAcessadas = new Stack<string>();

            paginasAcessadas.Push("www.google.com");
            Console.WriteLine("Acessando: www.google.com");

            paginasAcessadas.Push("www.github.com");
            Console.WriteLine("Acessando: www.github.com");

            paginasAcessadas.Push("www.stackoverflow.com");
            Console.WriteLine("Acessando: www.stackoverflow.com");

            Console.WriteLine($"\nPágina atual: {paginasAcessadas.Peek()}");

            Console.WriteLine("\nVoltando uma página...");
            string paginaAnterior = paginasAcessadas.Pop();
            Console.WriteLine($"Saiu de: {paginaAnterior}");
            Console.WriteLine($"Página atual: {paginasAcessadas.Peek()}");

            Console.WriteLine("\nVoltando outra página...");
            paginaAnterior = paginasAcessadas.Pop();
            Console.WriteLine($"Saiu de: {paginaAnterior}");
            Console.WriteLine($"Página atual: {paginasAcessadas.Peek()}");
        }
    }
}