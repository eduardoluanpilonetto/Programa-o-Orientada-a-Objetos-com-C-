class Program
{
    public static void Main(string[] args)
    {
        // Usando o construtor padrão
        Produto p1 = new Produto();

        // Usando a sobrecarga (só o nome)
        Produto p2 = new Produto("Mouse");

        // Usando o construtor completo
        Produto p3 = new Produto("Notebook", 3500.00, 10);

        p1.ExibirDados();
        p2.ExibirDados();
        p3.ExibirDados();

        using (Arquivo arquivo = new Arquivo("dados.txt"))
        {
            arquivo.Escrever("Olá, alunos!");
        }


        Console.WriteLine("\nFim do programa.");
    }
}

class Produto
{
    // Atributos
    public string Nome;
    public double Preco;
    public int Estoque; // inicialização automática (int começa em 0)

    // 1) Construtor padrão
    // Chama o construtor completo usando : this(...)
    public Produto() : this("Não informado", 0, 0)
    {
    }

    // 2) Construtor com parâmetros (completo)
    // Uso do "this" para diferenciar atributo de parâmetro
    public Produto(string nome, double preco, int estoque)
    {
        this.Nome = nome;
        this.Preco = preco;
        this.Estoque = estoque;
    }

    // 3) Sobrecarga de construtor (recebe só o nome)
    public Produto(string nome) : this(nome, 0, 0)
    {
    }

    // Método para exibir dados
    public void ExibirDados()
    {
        Console.WriteLine($"Produto: {Nome} | Preço: R${Preco:F2} | Estoque: {Estoque}");
    }

    // Destrutor
    // Executado automaticamente pelo Garbage Collector
    // antes do objeto ser removido da memória
    ~Produto()
    {
        Console.WriteLine($"Objeto '{Nome}' removido da memória.");
    }
}

class Arquivo : IDisposable
{
    private string nome;

    public Arquivo(string nome)
    {
        this.nome = nome;
        Console.WriteLine($"Arquivo '{nome}' foi ABERTO.");
    }

    public void Escrever(string texto)
    {
        Console.WriteLine($"Escrevendo no arquivo: {texto}");
    }

    public void Dispose()
    {
        Console.WriteLine($"Arquivo '{nome}' foi FECHADO.");
    }
}