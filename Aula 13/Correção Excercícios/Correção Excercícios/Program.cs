using System;
using System.Collections.Generic;

// ============================================================
//  AULA 13 - DESIGN PATTERNS COM C#
//  Classes.cs  ->  todas as classes dos exercicios
//  Program.cs  ->  instancias e testes
// ============================================================

namespace Aula13
{
    // ========================================================
    // EXERCICIO 1 - SINGLETON BASICO
    // ========================================================
    public sealed class ConfiguracaoSistema
    {
        // Unica instancia (guardada em um campo estatico privado)
        private static ConfiguracaoSistema _instancia;

        public string NomeAplicacao { get; set; }
        public string Versao { get; set; }

        // Construtor PRIVADO: ninguem de fora consegue fazer "new"
        private ConfiguracaoSistema()
        {
            NomeAplicacao = "Meu Sistema";
            Versao = "1.0.0";
        }

        // Ponto de acesso global
        public static ConfiguracaoSistema ObterInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new ConfiguracaoSistema();
            }
            return _instancia;
        }
    }

    // ========================================================
    // EXERCICIO 2 - SINGLETON THREAD SAFE (lock)
    // ========================================================
    public sealed class ConfiguracaoThreadSafe
    {
        private static ConfiguracaoThreadSafe _instancia;
        private static readonly object _trava = new object();
        private static int _totalCriadas = 0;

        public string NomeAplicacao { get; set; }

        // Conta quantas vezes o construtor realmente rodou (deve ser 1)
        public static int TotalInstanciasCriadas
        {
            get { return _totalCriadas; }
        }

        private ConfiguracaoThreadSafe()
        {
            _totalCriadas++;
            NomeAplicacao = "Sistema Multithread";
        }

        public static ConfiguracaoThreadSafe ObterInstancia()
        {
            // 1a checagem (sem lock) -> evita custo do lock depois que a instancia existe
            if (_instancia == null)
            {
                lock (_trava)
                {
                    // 2a checagem (com lock) -> garante que so UMA thread cria
                    if (_instancia == null)
                    {
                        _instancia = new ConfiguracaoThreadSafe();
                    }
                }
            }
            return _instancia;
        }
    }

    // ========================================================
    // EXERCICIO 3 - FACTORY DE VEICULOS
    // ========================================================
    public interface IVeiculo
    {
        string Tipo { get; }
        void Acelerar();
    }

    public class Carro : IVeiculo
    {
        public string Tipo { get { return "Carro"; } }
        public void Acelerar() { Console.WriteLine("  Carro acelerando: vrum vrum!"); }
    }

    public class Moto : IVeiculo
    {
        public string Tipo { get { return "Moto"; } }
        public void Acelerar() { Console.WriteLine("  Moto acelerando: brrrrum!"); }
    }

    public class Caminhao : IVeiculo
    {
        public string Tipo { get { return "Caminhao"; } }
        public void Acelerar() { Console.WriteLine("  Caminhao acelerando: buuuum!"); }
    }

    public static class VeiculoFactory
    {
        // O cliente so passa uma string e recebe um IVeiculo,
        // sem conhecer as classes concretas.
        public static IVeiculo CriarVeiculo(string tipo)
        {
            switch (tipo.ToLower())
            {
                case "carro": return new Carro();
                case "moto": return new Moto();
                case "caminhao": return new Caminhao();
                default:
                    throw new ArgumentException("Tipo de veiculo invalido: " + tipo);
            }
        }
    }

    // ========================================================
    // EXERCICIO 4 - FACTORY DE PAGAMENTOS
    // ========================================================
    public interface IPagamento
    {
        void ProcessarPagamento();
    }

    public class Pix : IPagamento
    {
        public void ProcessarPagamento() { Console.WriteLine("  Pagamento via PIX processado (instantaneo)."); }
    }

    public class Cartao : IPagamento
    {
        public void ProcessarPagamento() { Console.WriteLine("  Pagamento via CARTAO processado (aprovado na operadora)."); }
    }

    public class Boleto : IPagamento
    {
        public void ProcessarPagamento() { Console.WriteLine("  BOLETO gerado (compensacao em ate 3 dias uteis)."); }
    }

    public static class PagamentoFactory
    {
        public static IPagamento CriarPagamento(string metodo)
        {
            switch (metodo.ToLower())
            {
                case "pix": return new Pix();
                case "cartao": return new Cartao();
                case "boleto": return new Boleto();
                default:
                    throw new ArgumentException("Metodo de pagamento invalido: " + metodo);
            }
        }
    }

    // ========================================================
    // EXERCICIO 5 - OBSERVER DE NOTIFICACOES (CANAL DO YOUTUBE)
    // ========================================================
    public interface IObserver
    {
        void Atualizar(string mensagem);
    }

    public class Inscrito : IObserver
    {
        public string Nome { get; private set; }

        public Inscrito(string nome)
        {
            Nome = nome;
        }

        public void Atualizar(string mensagem)
        {
            Console.WriteLine("  [" + Nome + "] recebeu: " + mensagem);
        }
    }

    // Subject (observavel)
    public class Canal
    {
        private readonly List<IObserver> _inscritos = new List<IObserver>();
        public string Nome { get; private set; }

        public Canal(string nome)
        {
            Nome = nome;
        }

        public void Inscrever(IObserver observador)
        {
            if (!_inscritos.Contains(observador))
            {
                _inscritos.Add(observador);
            }
        }

        public void Remover(IObserver observador)
        {
            _inscritos.Remove(observador);
        }

        public void Notificar(string mensagem)
        {
            foreach (IObserver inscrito in _inscritos)
            {
                inscrito.Atualizar(mensagem);
            }
        }

        // Quando algo acontece no canal, avisa todo mundo automaticamente
        public void PublicarVideo(string titulo)
        {
            Console.WriteLine("  Canal '" + Nome + "' publicou um video: " + titulo);
            Notificar("Novo video no canal " + Nome + ": " + titulo);
        }
    }

    // ========================================================
    // EXERCICIO 6 - OBSERVER EM ESTOQUE
    // ========================================================
    public interface IObservadorEstoque
    {
        void ProdutoDisponivel(ProdutoObservavel produto);
    }

    public class ClienteEstoque : IObservadorEstoque
    {
        public string Nome { get; private set; }

        public ClienteEstoque(string nome)
        {
            Nome = nome;
        }

        public void ProdutoDisponivel(ProdutoObservavel produto)
        {
            Console.WriteLine("  Ola, " + Nome + "! O produto '" + produto.Nome + "' voltou ao estoque (" + produto.Quantidade + " un.).");
        }
    }

    // Produto observado
    public class ProdutoObservavel
    {
        private readonly List<IObservadorEstoque> _observadores = new List<IObservadorEstoque>();
        private int _quantidade;

        public string Nome { get; private set; }

        public int Quantidade
        {
            get { return _quantidade; }
            set
            {
                int anterior = _quantidade;
                _quantidade = value;

                // Estava zerado e voltou a ter estoque -> notifica
                if (anterior == 0 && _quantidade > 0)
                {
                    NotificarObservadores();
                }
            }
        }

        public ProdutoObservavel(string nome, int quantidade)
        {
            Nome = nome;
            _quantidade = quantidade;
        }

        public void Cadastrar(IObservadorEstoque observador)
        {
            _observadores.Add(observador);
        }

        public void Descadastrar(IObservadorEstoque observador)
        {
            _observadores.Remove(observador);
        }

        private void NotificarObservadores()
        {
            foreach (IObservadorEstoque o in _observadores)
            {
                o.ProdutoDisponivel(this);
            }
        }
    }

    // ========================================================
    // EXERCICIO 7 - MVC BASICO
    // ========================================================

    // MODEL
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        public Produto(int id, string nome, decimal preco)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
        }
    }

    // VIEW (so exibe, nao tem regra de negocio)
    public class ProdutoView
    {
        public void ExibirProduto(Produto produto)
        {
            Console.WriteLine("  ----- PRODUTO -----");
            Console.WriteLine("  Id.....: " + produto.Id);
            Console.WriteLine("  Nome...: " + produto.Nome);
            Console.WriteLine("  Preco..: " + produto.Preco.ToString("C"));
        }
    }

    // CONTROLLER (liga Model e View)
    public class ProdutoController
    {
        private readonly Produto _model;
        private readonly ProdutoView _view;

        public ProdutoController(Produto model, ProdutoView view)
        {
            _model = model;
            _view = view;
        }

        public void AtualizarPreco(decimal novoPreco)
        {
            if (novoPreco <= 0)
            {
                Console.WriteLine("  Preco invalido!");
                return;
            }
            _model.Preco = novoPreco;
        }

        public void ExibirProduto()
        {
            _view.ExibirProduto(_model);
        }
    }

    // ========================================================
    // EXERCICIO 8 - MVC DE CADASTRO DE USUARIOS
    // ========================================================

    // MODEL
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public Usuario(int id, string nome, string email)
        {
            Id = id;
            Nome = nome;
            Email = email;
        }
    }

    // VIEW (Console)
    public class UsuarioView
    {
        public void ExibirLista(List<Usuario> usuarios)
        {
            Console.WriteLine("  ----- LISTA DE USUARIOS -----");
            if (usuarios.Count == 0)
            {
                Console.WriteLine("  (nenhum usuario cadastrado)");
                return;
            }
            foreach (Usuario u in usuarios)
            {
                Console.WriteLine("  " + u.Id + " - " + u.Nome);
            }
        }

        public void ExibirDetalhes(Usuario usuario)
        {
            Console.WriteLine("  ----- DETALHES -----");
            Console.WriteLine("  Id....: " + usuario.Id);
            Console.WriteLine("  Nome..: " + usuario.Nome);
            Console.WriteLine("  Email.: " + usuario.Email);
        }

        public void ExibirMensagem(string mensagem)
        {
            Console.WriteLine("  >> " + mensagem);
        }
    }

    // CONTROLLER
    public class UsuarioController
    {
        private readonly List<Usuario> _usuarios = new List<Usuario>();
        private readonly UsuarioView _view;
        private int _proximoId = 1;

        public UsuarioController(UsuarioView view)
        {
            _view = view;
        }

        public void Cadastrar(string nome, string email)
        {
            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(email))
            {
                _view.ExibirMensagem("Nome e email sao obrigatorios.");
                return;
            }

            Usuario usuario = new Usuario(_proximoId++, nome, email);
            _usuarios.Add(usuario);
            _view.ExibirMensagem("Usuario '" + nome + "' cadastrado com sucesso!");
        }

        public void Listar()
        {
            _view.ExibirLista(_usuarios);
        }

        public void ExibirDetalhes(int id)
        {
            Usuario usuario = _usuarios.Find(u => u.Id == id);
            if (usuario == null)
            {
                _view.ExibirMensagem("Usuario " + id + " nao encontrado.");
                return;
            }
            _view.ExibirDetalhes(usuario);
        }
    }
}

// ============================================================
// EXERCICIO 9 - INTEGRACAO: E-COMMERCE
// Singleton (config) + Factory (pagamento) + Observer (pedido) + MVC
// ============================================================
namespace Aula13.Ecommerce
{
    // SINGLETON - configuracao da loja
    public sealed class ConfiguracaoLoja
    {
        private static ConfiguracaoLoja _instancia;

        public string NomeLoja { get; private set; }
        public decimal TaxaFrete { get; private set; }

        private ConfiguracaoLoja()
        {
            NomeLoja = "Loja do Professor";
            TaxaFrete = 15.00m;
        }

        public static ConfiguracaoLoja Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new ConfiguracaoLoja();
                }
                return _instancia;
            }
        }
    }

    // OBSERVER (cliente que acompanha o pedido) - reaproveita IObserver
    public class ClientePedido : IObserver
    {
        public string Nome { get; private set; }

        public ClientePedido(string nome)
        {
            Nome = nome;
        }

        public void Atualizar(string mensagem)
        {
            Console.WriteLine("  [Notificacao para " + Nome + "] " + mensagem);
        }
    }

    // MODEL + SUBJECT (observavel)
    public class Pedido
    {
        private readonly List<IObserver> _observadores = new List<IObserver>();

        public int Id { get; private set; }
        public string Cliente { get; private set; }
        public decimal ValorProdutos { get; private set; }
        public string Status { get; private set; }

        public decimal Total
        {
            get { return ValorProdutos + ConfiguracaoLoja.Instancia.TaxaFrete; }
        }

        public Pedido(int id, string cliente, decimal valorProdutos)
        {
            Id = id;
            Cliente = cliente;
            ValorProdutos = valorProdutos;
            Status = "Criado";
        }

        public void Inscrever(IObserver o) { _observadores.Add(o); }
        public void Remover(IObserver o) { _observadores.Remove(o); }

        public void MudarStatus(string novoStatus)
        {
            Status = novoStatus;
            foreach (IObserver o in _observadores)
            {
                o.Atualizar("Pedido #" + Id + " agora esta: " + Status);
            }
        }
    }

    // VIEW
    public class PedidoView
    {
        public void ExibirResumo(Pedido pedido)
        {
            Console.WriteLine("  ----- " + ConfiguracaoLoja.Instancia.NomeLoja + " -----");
            Console.WriteLine("  Pedido #" + pedido.Id + " | Cliente: " + pedido.Cliente);
            Console.WriteLine("  Produtos: " + pedido.ValorProdutos.ToString("C") +
                              " | Frete: " + ConfiguracaoLoja.Instancia.TaxaFrete.ToString("C") +
                              " | Total: " + pedido.Total.ToString("C"));
            Console.WriteLine("  Status: " + pedido.Status);
        }
    }

    // CONTROLLER
    public class PedidoController
    {
        private readonly PedidoView _view;
        private int _proximoId = 1;

        public PedidoController(PedidoView view)
        {
            _view = view;
        }

        public Pedido CriarPedido(string cliente, decimal valor)
        {
            Pedido pedido = new Pedido(_proximoId++, cliente, valor);
            // O cliente passa a observar o proprio pedido
            pedido.Inscrever(new ClientePedido(cliente));
            _view.ExibirResumo(pedido);
            return pedido;
        }

        public void Pagar(Pedido pedido, string metodo)
        {
            // FACTORY escolhe o meio de pagamento
            IPagamento pagamento = PagamentoFactory.CriarPagamento(metodo);
            pagamento.ProcessarPagamento();
            pedido.MudarStatus("Pago");
        }

        public void Enviar(Pedido pedido)
        {
            pedido.MudarStatus("Enviado");
        }

        public void Entregar(Pedido pedido)
        {
            pedido.MudarStatus("Entregue");
            _view.ExibirResumo(pedido);
        }
    }
}

// ============================================================
// EXERCICIO 10 - DESAFIO: SISTEMA DE DELIVERY
// Singleton (banco) + Factory (entregadores) + Observer (cliente) + MVC
// Extras: tratamento de erros, logs, simulacao de pedidos
// ============================================================
namespace Aula13.Delivery
{
    // ---------- LOG ----------
    public static class Logger
    {
        public static void Info(string msg)
        {
            Console.WriteLine("  [" + DateTime.Now.ToString("HH:mm:ss") + "][INFO ] " + msg);
        }

        public static void Erro(string msg)
        {
            Console.WriteLine("  [" + DateTime.Now.ToString("HH:mm:ss") + "][ERRO ] " + msg);
        }
    }

    // ---------- SINGLETON: CONEXAO COM O BANCO ----------
    public sealed class ConexaoBanco
    {
        private static ConexaoBanco _instancia;
        private static readonly object _trava = new object();

        public bool Conectada { get; private set; }

        private ConexaoBanco() { }

        public static ConexaoBanco Instancia
        {
            get
            {
                lock (_trava)
                {
                    if (_instancia == null)
                    {
                        _instancia = new ConexaoBanco();
                    }
                    return _instancia;
                }
            }
        }

        public void Conectar()
        {
            if (Conectada)
            {
                Logger.Info("Conexao com o banco ja estava aberta (reutilizando).");
                return;
            }
            Conectada = true;
            Logger.Info("Conexao com o banco aberta.");
        }

        public void Desconectar()
        {
            Conectada = false;
            Logger.Info("Conexao com o banco fechada.");
        }

        // Simula um comando SQL
        public void Executar(string comando)
        {
            if (!Conectada)
            {
                throw new InvalidOperationException("Banco desconectado. Chame Conectar() antes.");
            }
            Logger.Info("SQL> " + comando);
        }
    }

    // ---------- FACTORY: ENTREGADORES ----------
    public interface IEntregador
    {
        string Nome { get; }
        void Entregar(Pedido pedido);
    }

    public class EntregadorMoto : IEntregador
    {
        public string Nome { get { return "Moto"; } }

        public void Entregar(Pedido pedido)
        {
            Logger.Info("Moto levando o pedido #" + pedido.Id + " por " + pedido.DistanciaKm + " km.");
        }
    }

    public class EntregadorBicicleta : IEntregador
    {
        public string Nome { get { return "Bicicleta"; } }

        public void Entregar(Pedido pedido)
        {
            if (pedido.DistanciaKm > 5)
            {
                throw new InvalidOperationException("Bicicleta nao faz entregas acima de 5 km (pedido #" + pedido.Id + ").");
            }
            Logger.Info("Bicicleta pedalando com o pedido #" + pedido.Id + ".");
        }
    }

    public class EntregadorDrone : IEntregador
    {
        public string Nome { get { return "Drone"; } }

        public void Entregar(Pedido pedido)
        {
            if (pedido.DistanciaKm > 10)
            {
                throw new InvalidOperationException("Drone nao alcanca " + pedido.DistanciaKm + " km (pedido #" + pedido.Id + ").");
            }
            Logger.Info("Drone voando com o pedido #" + pedido.Id + ".");
        }
    }

    public static class EntregadorFactory
    {
        public static IEntregador Criar(string tipo)
        {
            switch (tipo.ToLower())
            {
                case "moto": return new EntregadorMoto();
                case "bicicleta": return new EntregadorBicicleta();
                case "drone": return new EntregadorDrone();
                default:
                    throw new ArgumentException("Tipo de entregador invalido: " + tipo);
            }
        }
    }

    // ---------- OBSERVER: CLIENTE ----------
    public class ClienteDelivery : IObserver
    {
        public string Nome { get; private set; }

        public ClienteDelivery(string nome)
        {
            Nome = nome;
        }

        public void Atualizar(string mensagem)
        {
            Console.WriteLine("  [Push para " + Nome + "] " + mensagem);
        }
    }

    // ---------- MODEL (+ SUBJECT) ----------
    public class Pedido
    {
        private readonly List<IObserver> _observadores = new List<IObserver>();

        public int Id { get; private set; }
        public string Cliente { get; private set; }
        public string Endereco { get; private set; }
        public double DistanciaKm { get; private set; }
        public string Status { get; private set; }

        public Pedido(int id, string cliente, string endereco, double distanciaKm)
        {
            Id = id;
            Cliente = cliente;
            Endereco = endereco;
            DistanciaKm = distanciaKm;
            Status = "Recebido";
        }

        public void Inscrever(IObserver o) { _observadores.Add(o); }
        public void Remover(IObserver o) { _observadores.Remove(o); }

        public void MudarStatus(string novoStatus)
        {
            Status = novoStatus;
            foreach (IObserver o in _observadores)
            {
                o.Atualizar("Pedido #" + Id + ": " + novoStatus);
            }
        }
    }

    // ---------- VIEW ----------
    public class PedidoView
    {
        public void ExibirPedido(Pedido p)
        {
            Console.WriteLine("  Pedido #" + p.Id + " | " + p.Cliente + " | " + p.Endereco +
                              " | " + p.DistanciaKm + " km | Status: " + p.Status);
        }

        public void ExibirMensagem(string msg)
        {
            Console.WriteLine("  >> " + msg);
        }
    }

    // ---------- CONTROLLER ----------
    public class PedidoController
    {
        private readonly PedidoView _view;
        private readonly List<Pedido> _pedidos = new List<Pedido>();
        private int _proximoId = 1;

        public PedidoController(PedidoView view)
        {
            _view = view;
        }

        public Pedido CriarPedido(string cliente, string endereco, double distanciaKm)
        {
            ConexaoBanco banco = ConexaoBanco.Instancia;
            banco.Conectar();

            Pedido pedido = new Pedido(_proximoId++, cliente, endereco, distanciaKm);
            pedido.Inscrever(new ClienteDelivery(cliente));
            _pedidos.Add(pedido);

            banco.Executar("INSERT INTO Pedidos (Id, Cliente, Endereco) VALUES (" +
                           pedido.Id + ", '" + cliente + "', '" + endereco + "')");
            Logger.Info("Pedido #" + pedido.Id + " criado.");
            return pedido;
        }

        public void Despachar(Pedido pedido, string tipoEntregador)
        {
            try
            {
                IEntregador entregador = EntregadorFactory.Criar(tipoEntregador);
                pedido.MudarStatus("saiu para entrega (" + entregador.Nome + ")");
                entregador.Entregar(pedido);
                pedido.MudarStatus("chegou ao destino");

                ConexaoBanco.Instancia.Executar("UPDATE Pedidos SET Status = 'Entregue' WHERE Id = " + pedido.Id);
            }
            catch (InvalidOperationException ex)
            {
                // Entregador nao consegue cumprir -> loga e usa a moto como plano B
                Logger.Erro(ex.Message);
                if (tipoEntregador.ToLower() == "moto")
                {
                    // Ja era o plano B: nao tenta de novo (evita loop infinito)
                    _view.ExibirMensagem("Pedido #" + pedido.Id + " nao pode ser despachado.");
                    return;
                }
                _view.ExibirMensagem("Trocando para Moto no pedido #" + pedido.Id + "...");
                Despachar(pedido, "moto");
            }
            catch (ArgumentException ex)
            {
                Logger.Erro(ex.Message);
                _view.ExibirMensagem("Pedido #" + pedido.Id + " nao pode ser despachado.");
            }
        }

        // Simulacao de varios pedidos
        public void SimularPedidos()
        {
            Pedido p1 = CriarPedido("Ana", "Rua A, 100", 3);
            Pedido p2 = CriarPedido("Bruno", "Av. B, 200", 8);
            Pedido p3 = CriarPedido("Carla", "Rua C, 300", 25);
            Pedido p4 = CriarPedido("Diego", "Rua D, 400", 2);

            Despachar(p1, "bicicleta");  // ok
            Despachar(p2, "drone");      // ok
            Despachar(p3, "drone");      // erro -> troca para moto
            Despachar(p4, "patinete");   // tipo invalido -> erro tratado

            Console.WriteLine();
            Console.WriteLine("  --- Situacao final ---");
            foreach (Pedido p in _pedidos)
            {
                _view.ExibirPedido(p);
            }
        }
    }
}