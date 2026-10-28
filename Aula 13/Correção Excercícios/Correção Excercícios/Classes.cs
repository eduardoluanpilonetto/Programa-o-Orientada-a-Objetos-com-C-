using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Aula13;
using Eco = Aula13.Ecommerce;
using Del = Aula13.Delivery;

// ============================================================
//  AULA 13 - DESIGN PATTERNS COM C#
//  Program.cs -> instancias e testes (as classes estao em Classes.cs)
// ============================================================

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Exercicio1();
        Exercicio2();
        Exercicio3();
        Exercicio4();
        Exercicio5();
        Exercicio6();
        Exercicio7();
        Exercicio8();
        Exercicio9();
        Exercicio10();

        Console.WriteLine();
        Console.WriteLine("Fim! Pressione qualquer tecla para sair...");
        Console.ReadKey();
    }

    static void Titulo(string texto)
    {
        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine(" " + texto);
        Console.WriteLine("==================================================");
    }

    // ---------- EXERCICIO 1 ----------
    static void Exercicio1()
    {
        Titulo("Exercicio 1 - Singleton Basico");

        // ConfiguracaoSistema c = new ConfiguracaoSistema();  // ERRO: construtor privado

        ConfiguracaoSistema a = ConfiguracaoSistema.ObterInstancia();
        ConfiguracaoSistema b = ConfiguracaoSistema.ObterInstancia();
        ConfiguracaoSistema c = ConfiguracaoSistema.ObterInstancia();

        a.NomeAplicacao = "Sistema Escolar";
        a.Versao = "2.5.1";

        // Alterou em "a", mas "b" e "c" enxergam a mesma mudanca
        Console.WriteLine("  b.NomeAplicacao = " + b.NomeAplicacao);
        Console.WriteLine("  c.Versao        = " + c.Versao);
        Console.WriteLine("  a == b ? " + ReferenceEquals(a, b));
        Console.WriteLine("  b == c ? " + ReferenceEquals(b, c));
    }

    // ---------- EXERCICIO 2 ----------
    static void Exercicio2()
    {
        Titulo("Exercicio 2 - Singleton Thread Safe");

        ConcurrentBag<ConfiguracaoThreadSafe> instancias = new ConcurrentBag<ConfiguracaoThreadSafe>();
        List<Thread> threads = new List<Thread>();

        for (int i = 0; i < 20; i++)
        {
            Thread t = new Thread(() =>
            {
                instancias.Add(ConfiguracaoThreadSafe.ObterInstancia());
            });
            threads.Add(t);
        }

        foreach (Thread t in threads) t.Start();
        foreach (Thread t in threads) t.Join();

        HashSet<ConfiguracaoThreadSafe> distintas = new HashSet<ConfiguracaoThreadSafe>(instancias);

        Console.WriteLine("  Threads executadas.............: " + threads.Count);
        Console.WriteLine("  Objetos DISTINTOS obtidos......: " + distintas.Count);
        Console.WriteLine("  Vezes que o construtor rodou...: " + ConfiguracaoThreadSafe.TotalInstanciasCriadas);
        Console.WriteLine(distintas.Count == 1
            ? "  OK: apenas um objeto foi criado!"
            : "  FALHA: mais de um objeto foi criado.");
    }

    // ---------- EXERCICIO 3 ----------
    static void Exercicio3()
    {
        Titulo("Exercicio 3 - Factory de Veiculos");

        string[] tipos = { "carro", "moto", "caminhao", "aviao" };

        foreach (string tipo in tipos)
        {
            try
            {
                IVeiculo veiculo = VeiculoFactory.CriarVeiculo(tipo);
                Console.WriteLine("  Criado: " + veiculo.Tipo);
                veiculo.Acelerar();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("  Erro: " + ex.Message);
            }
        }
    }

    // ---------- EXERCICIO 4 ----------
    static void Exercicio4()
    {
        Titulo("Exercicio 4 - Factory de Pagamentos");

        string[] metodos = { "pix", "cartao", "boleto", "dinheiro" };

        foreach (string metodo in metodos)
        {
            try
            {
                Console.WriteLine("  Metodo escolhido: " + metodo);
                IPagamento pagamento = PagamentoFactory.CriarPagamento(metodo);
                pagamento.ProcessarPagamento();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("  Erro: " + ex.Message);
            }
        }
    }

    // ---------- EXERCICIO 5 ----------
    static void Exercicio5()
    {
        Titulo("Exercicio 5 - Observer de Notificacoes");

        Canal canal = new Canal("Programando com Professor");

        Inscrito maria = new Inscrito("Maria");
        Inscrito joao = new Inscrito("Joao");
        Inscrito lucas = new Inscrito("Lucas");

        canal.Inscrever(maria);
        canal.Inscrever(joao);
        canal.Inscrever(lucas);

        canal.PublicarVideo("Design Patterns em C#");

        Console.WriteLine();
        Console.WriteLine("  (Joao cancelou a inscricao)");
        canal.Remover(joao);

        canal.PublicarVideo("Observer na pratica");
    }

    // ---------- EXERCICIO 6 ----------
    static void Exercicio6()
    {
        Titulo("Exercicio 6 - Observer em Estoque");

        ProdutoObservavel notebook = new ProdutoObservavel("Notebook Gamer", 0);

        notebook.Cadastrar(new ClienteEstoque("Fernanda"));
        notebook.Cadastrar(new ClienteEstoque("Rafael"));

        Console.WriteLine("  Estoque atual: " + notebook.Quantidade + " (esgotado)");
        Console.WriteLine("  Reabastecendo...");
        notebook.Quantidade = 10;   // dispara a notificacao automaticamente

        Console.WriteLine();
        Console.WriteLine("  Vendendo 2 unidades (nao notifica, ja havia estoque)...");
        notebook.Quantidade = 8;
    }

    // ---------- EXERCICIO 7 ----------
    static void Exercicio7()
    {
        Titulo("Exercicio 7 - MVC Basico");

        Produto model = new Produto(1, "Teclado Mecanico", 250.00m);
        ProdutoView view = new ProdutoView();
        ProdutoController controller = new ProdutoController(model, view);

        controller.ExibirProduto();

        Console.WriteLine();
        Console.WriteLine("  Atualizando preco para 199,90...");
        controller.AtualizarPreco(199.90m);
        controller.ExibirProduto();
    }

    // ---------- EXERCICIO 8 ----------
    static void Exercicio8()
    {
        Titulo("Exercicio 8 - MVC de Cadastro de Usuarios");

        UsuarioView view = new UsuarioView();
        UsuarioController controller = new UsuarioController(view);

        controller.Cadastrar("Ana Souza", "ana@email.com");
        controller.Cadastrar("Carlos Lima", "carlos@email.com");
        controller.Cadastrar("", "invalido@email.com");   // validacao

        Console.WriteLine();
        controller.Listar();

        Console.WriteLine();
        controller.ExibirDetalhes(2);
        controller.ExibirDetalhes(99);   // nao existe
    }

    // ---------- EXERCICIO 9 ----------
    static void Exercicio9()
    {
        Titulo("Exercicio 9 - Integracao de Patterns (E-commerce)");

        // Singleton: a mesma configuracao em qualquer lugar
        Console.WriteLine("  Loja: " + Eco.ConfiguracaoLoja.Instancia.NomeLoja);

        Eco.PedidoView view = new Eco.PedidoView();
        Eco.PedidoController controller = new Eco.PedidoController(view);

        Eco.Pedido pedido = controller.CriarPedido("Mariana", 350.00m);

        Console.WriteLine();
        controller.Pagar(pedido, "pix");
        controller.Enviar(pedido);
        controller.Entregar(pedido);
    }

    // ---------- EXERCICIO 10 ----------
    static void Exercicio10()
    {
        Titulo("Exercicio 10 - Desafio Avancado (Delivery)");

        Del.PedidoView view = new Del.PedidoView();
        Del.PedidoController controller = new Del.PedidoController(view);

        controller.SimularPedidos();

        Del.ConexaoBanco.Instancia.Desconectar();
    }
}