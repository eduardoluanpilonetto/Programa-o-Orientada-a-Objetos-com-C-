using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

// ---------------------------------------------------------------------
// EXERCÍCIO 1 – Identificando responsabilidades (SRP)
// ---------------------------------------------------------------------
// ANTES (violação):
//   public class Pedido
//   {
//       public void SalvarPedido() { }        // 1) persistência
//       public void EnviarEmailCliente() { }  // 2) comunicação
//       public void GerarNotaFiscal() { }     // 3) fiscal
//   }
// Resposta: a classe possui 3 responsabilidades (e mais uma implícita:
// representar os dados do pedido). Cada uma muda por um motivo diferente.
// DEPOIS: cada responsabilidade vai para uma classe específica.
namespace Ex01_SRP
{
    public class Pedido
    {
        public int Id { get; set; }
        public string Cliente { get; set; } = "";
        public decimal Total { get; set; }
    }

    public class PedidoRepository
    {
        public void Salvar(Pedido pedido) =>
            Console.WriteLine($"[Repositório] Pedido {pedido.Id} salvo no banco.");
    }

    public class EmailClienteService
    {
        public void Enviar(Pedido pedido) =>
            Console.WriteLine($"[E-mail] Confirmação do pedido {pedido.Id} enviada para {pedido.Cliente}.");
    }

    public class NotaFiscalService
    {
        public void Gerar(Pedido pedido) =>
            Console.WriteLine($"[Nota Fiscal] NF gerada para o pedido {pedido.Id} no valor de {pedido.Total:C}.");
    }

    public static class Exercicio
    {
        public static void Executar()
        {
            var pedido = new Pedido { Id = 1, Cliente = "Maria", Total = 250m };

            new PedidoRepository().Salvar(pedido);
            new EmailClienteService().Enviar(pedido);
            new NotaFiscalService().Gerar(pedido);
        }
    }
}

// ---------------------------------------------------------------------
// EXERCÍCIO 2 – Refatoração de relatório (SRP)
// ---------------------------------------------------------------------
// Uma classe gera o relatório, outra exporta para PDF e outra envia e-mail.
namespace Ex02_SRP
{
    public class Relatorio
    {
        public string Titulo { get; }
        public string Conteudo { get; }

        public Relatorio(string titulo, string conteudo)
        {
            Titulo = titulo;
            Conteudo = conteudo;
        }
    }

    public class GeradorRelatorio
    {
        public Relatorio Gerar(IEnumerable<decimal> vendas)
        {
            var lista = vendas.ToList();
            string conteudo = $"Vendas: {lista.Count} | Total: {lista.Sum():C}";
            return new Relatorio("Relatório de Vendas", conteudo);
        }
    }

    public class ExportadorPdf
    {
        public string Exportar(Relatorio relatorio)
        {
            string arquivo = $"{relatorio.Titulo.Replace(" ", "_")}.pdf";
            Console.WriteLine($"[PDF] Arquivo '{arquivo}' criado com o conteúdo: {relatorio.Conteudo}");
            return arquivo;
        }
    }

    public class EnviadorEmail
    {
        public void Enviar(string destinatario, string anexo) =>
            Console.WriteLine($"[E-mail] Enviado para {destinatario} com o anexo '{anexo}'.");
    }

    public static class Exercicio
    {
        public static void Executar()
        {
            var relatorio = new GeradorRelatorio().Gerar(new[] { 100m, 250.5m, 80m });
            string pdf = new ExportadorPdf().Exportar(relatorio);
            new EnviadorEmail().Enviar("gerente@empresa.com", pdf);
        }
    }
}

// ---------------------------------------------------------------------
// EXERCÍCIO 3 – Sistema de descontos (OCP)
// ---------------------------------------------------------------------
// Aberto para extensão (novas classes) e fechado para modificação
// (a CalculadoraDesconto nunca precisa ser alterada).
namespace Ex03_OCP
{
    public interface IDesconto
    {
        decimal Aplicar(decimal valor);
    }

    public class DescontoClienteComum : IDesconto
    {
        public decimal Aplicar(decimal valor) => valor; // sem desconto
    }

    public class DescontoClienteVip : IDesconto
    {
        public decimal Aplicar(decimal valor) => valor * 0.90m; // 10%
    }

    public class DescontoClientePremium : IDesconto
    {
        public decimal Aplicar(decimal valor) => valor * 0.80m; // 20%
    }

    // Novo tipo adicionado DEPOIS, sem tocar em nenhum código existente:
    public class DescontoBlackFriday : IDesconto
    {
        public decimal Aplicar(decimal valor) => valor * 0.70m; // 30%
    }

    public class CalculadoraDesconto
    {
        public decimal Calcular(decimal valor, IDesconto desconto) => desconto.Aplicar(valor);
    }

    public static class Exercicio
    {
        public static void Executar()
        {
            var calc = new CalculadoraDesconto();
            decimal valor = 1000m;

            var tipos = new Dictionary<string, IDesconto>
            {
                ["Comum"] = new DescontoClienteComum(),
                ["VIP"] = new DescontoClienteVip(),
                ["Premium"] = new DescontoClientePremium(),
                ["Black Friday (novo)"] = new DescontoBlackFriday()
            };

            foreach (var t in tipos)
                Console.WriteLine($"Cliente {t.Key,-20}: {valor:C} -> {calc.Calcular(valor, t.Value):C}");
        }
    }
}

// ---------------------------------------------------------------------
// EXERCÍCIO 4 – Calculadora de frete (OCP + polimorfismo)
// ---------------------------------------------------------------------
// Sem if/else: cada estratégia conhece a própria regra.
namespace Ex04_OCP
{
    public interface IFrete
    {
        string Nome { get; }
        decimal Calcular(decimal pesoKg);
    }

    public class Sedex : IFrete
    {
        public string Nome => "Sedex";
        public decimal Calcular(decimal pesoKg) => 25m + pesoKg * 5m;
    }

    public class Pac : IFrete
    {
        public string Nome => "PAC";
        public decimal Calcular(decimal pesoKg) => 12m + pesoKg * 2m;
    }

    public class RetiradaNaLoja : IFrete
    {
        public string Nome => "Retirada na loja";
        public decimal Calcular(decimal pesoKg) => 0m;
    }

    public class CalculadoraFrete
    {
        public decimal Calcular(IFrete frete, decimal pesoKg) => frete.Calcular(pesoKg);
    }

    public static class Exercicio
    {
        public static void Executar()
        {
            var calc = new CalculadoraFrete();
            IFrete[] opcoes = { new Sedex(), new Pac(), new RetiradaNaLoja() };

            foreach (var f in opcoes)
                Console.WriteLine($"{f.Nome,-18}: {calc.Calcular(f, 3m):C} (pacote de 3 kg)");
        }
    }
}

// ---------------------------------------------------------------------
// EXERCÍCIO 5 – Violação de Liskov (LSP)
// ---------------------------------------------------------------------
// Quadrado herda de Retangulo, mas precisa manter Largura == Altura.
// Ao sobrescrever os setters, ele muda o comportamento esperado de Retangulo.
namespace Ex05_LSP
{
    public class Retangulo
    {
        public virtual int Largura { get; set; }
        public virtual int Altura { get; set; }
        public int Area() => Largura * Altura;
    }

    public class Quadrado : Retangulo
    {
        public override int Largura
        {
            get => base.Largura;
            set { base.Largura = value; base.Altura = value; }
        }

        public override int Altura
        {
            get => base.Altura;
            set { base.Altura = value; base.Largura = value; }
        }
    }

    public static class Exercicio
    {
        // Este método funciona para QUALQUER retângulo... em teoria.
        private static void Testar(Retangulo r)
        {
            r.Largura = 5;
            r.Altura = 10;
            Console.WriteLine($"{r.GetType().Name,-10} -> esperado: 50 | obtido: {r.Area()}");
        }

        public static void Executar()
        {
            Testar(new Retangulo()); // 50 (ok)
            Testar(new Quadrado());  // 100 (quebrou o contrato!)

            Console.WriteLine("Problema: Quadrado NÃO é substituível por Retangulo (viola o LSP).");
        }
    }
}

// ---------------------------------------------------------------------
// EXERCÍCIO 6 – Modelagem correta com LSP
// ---------------------------------------------------------------------
// Solução: herança não era necessária. As duas formas implementam uma
// abstração comum (IForma) e cada uma tem as próprias regras.
namespace Ex06_LSP
{
    public interface IForma
    {
        double Area();
    }

    public class Retangulo : IForma
    {
        public int Largura { get; }
        public int Altura { get; }

        public Retangulo(int largura, int altura)
        {
            Largura = largura;
            Altura = altura;
        }

        public double Area() => Largura * Altura;
    }

    public class Quadrado : IForma
    {
        public int Lado { get; }

        public Quadrado(int lado) => Lado = lado;

        public double Area() => Lado * Lado;
    }

    public static class Exercicio
    {
        public static void Executar()
        {
            IForma[] formas = { new Retangulo(5, 10), new Quadrado(4) };

            foreach (var f in formas)
                Console.WriteLine($"{f.GetType().Name,-10} -> área: {f.Area()}");
        }
    }
}

// ---------------------------------------------------------------------
// EXERCÍCIO 7 – Interface grande demais (ISP)
// ---------------------------------------------------------------------
// ANTES: IFuncionario { Trabalhar(); Comer(); Dirigir(); }
// Um Robô seria obrigado a implementar Comer(), e quem não dirige a Dirigir().
namespace Ex07_ISP
{
    public interface ITrabalhador { void Trabalhar(); }
    public interface IAlimentavel { void Comer(); }
    public interface IMotorista { void Dirigir(); }

    public class Desenvolvedor : ITrabalhador, IAlimentavel
    {
        public void Trabalhar() => Console.WriteLine("Desenvolvedor: programando.");
        public void Comer() => Console.WriteLine("Desenvolvedor: almoçando.");
    }

    public class Motorista : ITrabalhador, IAlimentavel, IMotorista
    {
        public void Trabalhar() => Console.WriteLine("Motorista: entregando encomendas.");
        public void Comer() => Console.WriteLine("Motorista: fazendo uma pausa para lanchar.");
        public void Dirigir() => Console.WriteLine("Motorista: dirigindo o caminhão.");
    }

    public class RoboDeLinha : ITrabalhador // não come e não dirige
    {
        public void Trabalhar() => Console.WriteLine("Robô: montando peças 24h.");
    }

    public static class Exercicio
    {
        public static void Executar()
        {
            var trabalhadores = new List<ITrabalhador>
            {
                new Desenvolvedor(), new Motorista(), new RoboDeLinha()
            };
            trabalhadores.ForEach(t => t.Trabalhar());

            new Motorista().Dirigir();
        }
    }
}

// ---------------------------------------------------------------------
// EXERCÍCIO 8 – Impressora multifuncional (ISP)
// ---------------------------------------------------------------------
namespace Ex08_ISP
{
    public interface IImpressora { void Imprimir(string documento); }
    public interface IScanner { string Escanear(); }

    public class ImpressoraSimples : IImpressora
    {
        public void Imprimir(string documento) =>
            Console.WriteLine($"[Impressora] Imprimindo: {documento}");
    }

    public class ScannerSimples : IScanner
    {
        public string Escanear()
        {
            Console.WriteLine("[Scanner] Documento escaneado.");
            return "documento_escaneado.png";
        }
    }

    public class Multifuncional : IImpressora, IScanner
    {
        public void Imprimir(string documento) =>
            Console.WriteLine($"[Multifuncional] Imprimindo: {documento}");

        public string Escanear()
        {
            Console.WriteLine("[Multifuncional] Documento escaneado.");
            return "scan_multifuncional.png";
        }
    }

    public static class Exercicio
    {
        public static void Executar()
        {
            new ImpressoraSimples().Imprimir("Contrato.docx");
            new ScannerSimples().Escanear();

            var multi = new Multifuncional();
            multi.Imprimir("Relatório.pdf");
            multi.Escanear();
        }
    }
}

// ---------------------------------------------------------------------
// EXERCÍCIO 9 – Dependência forte (DIP)
// ---------------------------------------------------------------------
// ANTES: private EmailService _email = new EmailService();
//   -> PedidoService depende de uma classe concreta, criada por ele mesmo.
//      Não dá para trocar por SMS nem testar com um "falso".
// DEPOIS: depende de uma abstração, recebida por injeção de dependência.
namespace Ex09_DIP
{
    public interface INotificador
    {
        void Enviar(string destino, string mensagem);
    }

    public class EmailService : INotificador
    {
        public void Enviar(string destino, string mensagem) =>
            Console.WriteLine($"[E-mail para {destino}] {mensagem}");
    }

    public class SmsService : INotificador
    {
        public void Enviar(string destino, string mensagem) =>
            Console.WriteLine($"[SMS para {destino}] {mensagem}");
    }

    // Útil em testes: não envia nada, apenas guarda o que seria enviado.
    public class NotificadorFalso : INotificador
    {
        public List<string> Mensagens { get; } = new();
        public void Enviar(string destino, string mensagem) => Mensagens.Add($"{destino}: {mensagem}");
    }

    public class PedidoService
    {
        private readonly INotificador _notificador;

        // Injeção de dependência via construtor
        public PedidoService(INotificador notificador) => _notificador = notificador;

        public void FinalizarPedido(int id, string contato) =>
            _notificador.Enviar(contato, $"Seu pedido {id} foi finalizado!");
    }

    public static class Exercicio
    {
        public static void Executar()
        {
            new PedidoService(new EmailService()).FinalizarPedido(10, "cliente@email.com");
            new PedidoService(new SmsService()).FinalizarPedido(11, "(49) 99999-0000");

            var falso = new NotificadorFalso();
            new PedidoService(falso).FinalizarPedido(12, "teste");
            Console.WriteLine($"[Teste] Mensagens capturadas pelo falso: {falso.Mensagens.Count}");
        }
    }
}

// ---------------------------------------------------------------------
// EXERCÍCIO 10 – Projeto Final SOLID: mini sistema de pedidos
// ---------------------------------------------------------------------
// S – cada classe tem uma única responsabilidade (cadastro, processamento,
//     notificação, relatório).
// O – novos descontos/notificadores/relatórios entram só com novas classes.
// L – qualquer INotificador / IDesconto / IRelatorio pode substituir outro.
// I – interfaces pequenas e específicas (leitura e escrita separadas).
// D – serviços dependem de abstrações, montadas na "raiz de composição" (Executar).
namespace Ex10_Projeto
{
    // ----- Modelos -----
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public string Email { get; set; } = "";
        public string Categoria { get; set; } = "Comum";
    }

    public class ItemPedido
    {
        public string Produto { get; set; } = "";
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal => Quantidade * PrecoUnitario;
    }

    public class Pedido
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; } = null!;
        public List<ItemPedido> Itens { get; } = new();
        public decimal Total { get; set; }
    }

    // ----- ISP: interfaces pequenas -----
    public interface IClienteLeitura { Cliente? ObterPorId(int id); IEnumerable<Cliente> ListarTodos(); }
    public interface IClienteEscrita { void Adicionar(Cliente cliente); }

    public interface IPedidoRepository { void Salvar(Pedido pedido); IEnumerable<Pedido> ListarTodos(); }
    public interface IDesconto { string Categoria { get; } decimal Aplicar(decimal valor); }
    public interface INotificador { void Notificar(Cliente cliente, string mensagem); }
    public interface IRelatorio { string Gerar(IEnumerable<Pedido> pedidos); }

    // ----- Repositórios em memória -----
    public class ClienteRepositoryMemoria : IClienteLeitura, IClienteEscrita
    {
        private readonly List<Cliente> _clientes = new();
        public void Adicionar(Cliente cliente) => _clientes.Add(cliente);
        public Cliente? ObterPorId(int id) => _clientes.FirstOrDefault(c => c.Id == id);
        public IEnumerable<Cliente> ListarTodos() => _clientes;
    }

    public class PedidoRepositoryMemoria : IPedidoRepository
    {
        private readonly List<Pedido> _pedidos = new();
        public void Salvar(Pedido pedido) => _pedidos.Add(pedido);
        public IEnumerable<Pedido> ListarTodos() => _pedidos;
    }

    // ----- Descontos (OCP/LSP) -----
    public class DescontoComum : IDesconto
    {
        public string Categoria => "Comum";
        public decimal Aplicar(decimal valor) => valor;
    }

    public class DescontoVip : IDesconto
    {
        public string Categoria => "VIP";
        public decimal Aplicar(decimal valor) => valor * 0.90m;
    }

    public class DescontoPremium : IDesconto
    {
        public string Categoria => "Premium";
        public decimal Aplicar(decimal valor) => valor * 0.80m;
    }

    // ----- Notificadores (OCP/LSP) -----
    public class NotificadorEmail : INotificador
    {
        public void Notificar(Cliente cliente, string mensagem) =>
            Console.WriteLine($"   [E-mail -> {cliente.Email}] {mensagem}");
    }

    public class NotificadorConsole : INotificador
    {
        public void Notificar(Cliente cliente, string mensagem) =>
            Console.WriteLine($"   [Log] Notificação para {cliente.Nome}: {mensagem}");
    }

    // ----- Serviços (SRP + DIP) -----
    public class CadastroClienteService
    {
        private readonly IClienteEscrita _escrita;
        public CadastroClienteService(IClienteEscrita escrita) => _escrita = escrita;

        public void Cadastrar(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nome))
                throw new ArgumentException("O nome do cliente é obrigatório.");

            _escrita.Adicionar(cliente);
        }
    }

    public class ProcessadorPedidos
    {
        private readonly IPedidoRepository _repositorio;
        private readonly IEnumerable<IDesconto> _descontos;
        private readonly IEnumerable<INotificador> _notificadores;

        public ProcessadorPedidos(
            IPedidoRepository repositorio,
            IEnumerable<IDesconto> descontos,
            IEnumerable<INotificador> notificadores)
        {
            _repositorio = repositorio;
            _descontos = descontos;
            _notificadores = notificadores;
        }

        public void Processar(Pedido pedido)
        {
            decimal bruto = pedido.Itens.Sum(i => i.Subtotal);

            // Sem if/else por categoria: escolhe a estratégia correspondente
            var desconto = _descontos.FirstOrDefault(d => d.Categoria == pedido.Cliente.Categoria)
                           ?? _descontos.First();

            pedido.Total = desconto.Aplicar(bruto);
            _repositorio.Salvar(pedido);

            foreach (var n in _notificadores)
                n.Notificar(pedido.Cliente, $"Pedido {pedido.Id} confirmado. Total: {pedido.Total:C}");
        }
    }

    // ----- Relatório (OCP) -----
    public class RelatorioTexto : IRelatorio
    {
        public string Gerar(IEnumerable<Pedido> pedidos)
        {
            var sb = new StringBuilder();
            sb.AppendLine("   --- RELATÓRIO DE PEDIDOS ---");
            foreach (var p in pedidos)
                sb.AppendLine($"   Pedido {p.Id} | {p.Cliente.Nome} ({p.Cliente.Categoria}) | {p.Total:C}");
            sb.Append($"   TOTAL GERAL: {pedidos.Sum(p => p.Total):C}");
            return sb.ToString();
        }
    }

    public class GeradorRelatorios
    {
        private readonly IPedidoRepository _repositorio;
        private readonly IRelatorio _formato;

        public GeradorRelatorios(IPedidoRepository repositorio, IRelatorio formato)
        {
            _repositorio = repositorio;
            _formato = formato;
        }

        public string Gerar() => _formato.Gerar(_repositorio.ListarTodos());
    }

    public static class Exercicio
    {
        public static void Executar()
        {
            // Raiz de composição: único lugar que conhece as classes concretas
            var clientesRepo = new ClienteRepositoryMemoria();
            var pedidosRepo = new PedidoRepositoryMemoria();

            var cadastro = new CadastroClienteService(clientesRepo);
            var processador = new ProcessadorPedidos(
                pedidosRepo,
                new IDesconto[] { new DescontoComum(), new DescontoVip(), new DescontoPremium() },
                new INotificador[] { new NotificadorEmail(), new NotificadorConsole() });
            var relatorios = new GeradorRelatorios(pedidosRepo, new RelatorioTexto());

            Console.WriteLine("1) Cadastro de clientes");
            cadastro.Cadastrar(new Cliente { Id = 1, Nome = "Ana", Email = "ana@email.com", Categoria = "VIP" });
            cadastro.Cadastrar(new Cliente { Id = 2, Nome = "Bruno", Email = "bruno@email.com", Categoria = "Comum" });
            cadastro.Cadastrar(new Cliente { Id = 3, Nome = "Carla", Email = "carla@email.com", Categoria = "Premium" });
            Console.WriteLine($"   {clientesRepo.ListarTodos().Count()} clientes cadastrados.");

            Console.WriteLine("\n2) Processamento de pedidos + 3) Notificações");
            int numero = 1;
            foreach (var cliente in clientesRepo.ListarTodos())
            {
                var pedido = new Pedido { Id = numero++, Cliente = cliente };
                pedido.Itens.Add(new ItemPedido { Produto = "Teclado", Quantidade = 1, PrecoUnitario = 200m });
                pedido.Itens.Add(new ItemPedido { Produto = "Mouse", Quantidade = 2, PrecoUnitario = 50m });
                processador.Processar(pedido);
            }

            Console.WriteLine("\n4) Relatório");
            Console.WriteLine(relatorios.Gerar());
        }
    }
}

// ---------------------------------------------------------------------
// DESAFIO EXTRA – Identificar violações, explicar e refatorar
// ---------------------------------------------------------------------
// Exemplo escolhido: classe "Pagamento" típica de projetos iniciantes.
//
// CÓDIGO ORIGINAL (com violações):
//
//   public class Pagamento
//   {
//       public void Processar(string tipo, decimal valor, string emailCliente)
//       {
//           if (tipo == "cartao")       { /* regra do cartão */ }
//           else if (tipo == "boleto")  { /* regra do boleto */ }
//           else if (tipo == "pix")     { /* regra do pix */ }
//
//           var smtp = new SmtpClient();           // cria dependência concreta
//           smtp.Send(emailCliente, "Pago!");      // envia e-mail
//           File.AppendAllText("log.txt", "...");  // grava log
//       }
//   }
//
// 1) VIOLAÇÕES IDENTIFICADAS
//   - SRP: processa pagamento, envia e-mail e grava log na mesma classe.
//   - OCP: para aceitar um novo meio de pagamento é preciso editar o if/else.
//   - DIP: depende diretamente de SmtpClient/File (classes concretas).
//   - (ISP/LSP não apareciam neste trecho, mas poderiam surgir ao criar
//     uma interface única "IPagamento" com métodos que nem todo meio usa.)
//
// 2) PROBLEMAS CAUSADOS
//   - Cada novo meio de pagamento exige mexer em código que já funciona
//     (risco de quebrar o que estava pronto).
//   - Impossível testar sem enviar e-mail real e escrever em arquivo.
//   - Uma mudança no e-mail ou no log obriga a mexer na classe de pagamento.
//
// 3) REFATORAÇÃO (abaixo)
namespace Desafio_Extra
{
    public interface IMeioPagamento
    {
        string Nome { get; }
        bool Pagar(decimal valor);
    }

    public class PagamentoCartao : IMeioPagamento
    {
        public string Nome => "Cartão";
        public bool Pagar(decimal valor)
        {
            Console.WriteLine($"   Cartão: cobrando {valor:C}...");
            return true;
        }
    }

    public class PagamentoBoleto : IMeioPagamento
    {
        public string Nome => "Boleto";
        public bool Pagar(decimal valor)
        {
            Console.WriteLine($"   Boleto: gerando cobrança de {valor:C}...");
            return true;
        }
    }

    public class PagamentoPix : IMeioPagamento
    {
        public string Nome => "Pix";
        public bool Pagar(decimal valor)
        {
            Console.WriteLine($"   Pix: QR Code gerado para {valor:C}...");
            return true;
        }
    }

    public interface INotificador { void Notificar(string destino, string mensagem); }
    public interface ILogger { void Registrar(string mensagem); }

    public class NotificadorEmail : INotificador
    {
        public void Notificar(string destino, string mensagem) =>
            Console.WriteLine($"   [E-mail -> {destino}] {mensagem}");
    }

    public class LoggerConsole : ILogger
    {
        public void Registrar(string mensagem) => Console.WriteLine($"   [LOG] {mensagem}");
    }

    public class ProcessadorPagamento
    {
        private readonly IMeioPagamento _meio;
        private readonly INotificador _notificador;
        private readonly ILogger _logger;

        public ProcessadorPagamento(IMeioPagamento meio, INotificador notificador, ILogger logger)
        {
            _meio = meio;
            _notificador = notificador;
            _logger = logger;
        }

        public void Processar(decimal valor, string emailCliente)
        {
            bool ok = _meio.Pagar(valor);
            _logger.Registrar($"Pagamento via {_meio.Nome}: {(ok ? "aprovado" : "recusado")}");

            if (ok)
                _notificador.Notificar(emailCliente, "Pagamento confirmado!");
        }
    }

    public static class Exercicio
    {
        public static void Executar()
        {
            var notificador = new NotificadorEmail();
            var logger = new LoggerConsole();

            IMeioPagamento[] meios = { new PagamentoCartao(), new PagamentoBoleto(), new PagamentoPix() };

            foreach (var meio in meios)
            {
                new ProcessadorPagamento(meio, notificador, logger).Processar(150m, "cliente@email.com");
                Console.WriteLine();
            }
        }
    }
}

// ---------------------------------------------------------------------
// PROGRAMA PRINCIPAL
// ---------------------------------------------------------------------
namespace AulaSolid
{
    public class Program
    {
        private static void Titulo(string texto)
        {
            Console.WriteLine();
            Console.WriteLine(new string('=', 70));
            Console.WriteLine(texto);
            Console.WriteLine(new string('=', 70));
        }

        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
            CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

            Titulo("EXERCÍCIO 1 – SRP: Identificando responsabilidades");
            Ex01_SRP.Exercicio.Executar();

            Titulo("EXERCÍCIO 2 – SRP: Refatoração de relatório");
            Ex02_SRP.Exercicio.Executar();

            Titulo("EXERCÍCIO 3 – OCP: Sistema de descontos");
            Ex03_OCP.Exercicio.Executar();

            Titulo("EXERCÍCIO 4 – OCP: Calculadora de frete");
            Ex04_OCP.Exercicio.Executar();

            Titulo("EXERCÍCIO 5 – LSP: Violação (Retângulo x Quadrado)");
            Ex05_LSP.Exercicio.Executar();

            Titulo("EXERCÍCIO 6 – LSP: Modelagem correta");
            Ex06_LSP.Exercicio.Executar();

            Titulo("EXERCÍCIO 7 – ISP: Interface grande demais");
            Ex07_ISP.Exercicio.Executar();

            Titulo("EXERCÍCIO 8 – ISP: Impressora multifuncional");
            Ex08_ISP.Exercicio.Executar();

            Titulo("EXERCÍCIO 9 – DIP: Dependência forte");
            Ex09_DIP.Exercicio.Executar();

            Titulo("EXERCÍCIO 10 – Projeto final SOLID");
            Ex10_Projeto.Exercicio.Executar();

            Titulo("DESAFIO EXTRA – Refatoração de um código com violações");
            Desafio_Extra.Exercicio.Executar();

            Console.WriteLine();
            Console.WriteLine("Fim da correção. Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}