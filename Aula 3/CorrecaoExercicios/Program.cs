using Classes;
class Program
{
    public static void Main(string[] args)
    {
        Pessoa pes = new Pessoa();
        pes.nome = "Duduzinho";
        pes.SetIdade(10);
        Console.WriteLine(pes.GetIdade());

        Produto produto = new Produto();
        produto.nome = "Sabonete";
        produto.Preco = 10;
        produto.AplicarDesconto(5);

        ContaBancaria contaBancaria = new ContaBancaria();
        contaBancaria.titular = "Pilonetto";
        contaBancaria.Depositar(10);
        Console.WriteLine(contaBancaria.Saldo);
        contaBancaria.Sacar(5);
        Console.WriteLine(contaBancaria.Saldo);
       
        Carro carro = new Carro();
        carro.Acelerar(100);
        Console.WriteLine(carro.Velocidade);
        carro.Acelerar(150);
        Console.WriteLine(carro.Velocidade);
        carro.Frear(50);
        Console.WriteLine(carro.Velocidade); 
        carro.Frear(300);
        Console.WriteLine(carro.Velocidade);

        Aluno aluno = new Aluno();
        aluno.Nome = "João";
        aluno.DefinirNotas(8.5, 7.0);
        Console.WriteLine("Nome: " + aluno.Nome);
        Console.WriteLine("Média: " + aluno.CalcularMedia());
        if (aluno.VerificarAprovacao())
            Console.WriteLine("Situação: Aprovado");
        else
            Console.WriteLine("Situação: Reprovado");

        Funcionario felipe = new Funcionario();
        felipe.Nome = "Felipe";
        felipe.Salario = -1;
        felipe.Salario = 200;
        felipe.AumentarSlario(20);


        var u = new User();
        u.Login = "Teste";
        u.Password = "123456";

        u.ExecuteLogin("teste", "1234567"); // erro
        u.ExecuteLogin("Teste", "123456"); // top show de bola

        Estoque estoque = new Estoque();
        estoque.AdicionaQtd(-1);
        estoque.AdicionaQtd(1000);
        estoque.RemoveQtd(2000);
        estoque.RemoveQtd(100);

        Retangulo ret = new Retangulo();
        ret.Altura = 10;
        ret.Largura = 20;
        Console.WriteLine($"Área: {ret.CalcularArea()}");
        Console.WriteLine($"Perimetro: {ret.CalcularPerimetro()}");

        Livro livro = new Livro();
        livro.Titulo = "Pilonetto, Melhor professor do mundo";
        livro.QuantidadeDisponivel = 10;
        livro.Emprestar(); // OK
        livro.Emprestar(); // OK
        livro.Emprestar(); // Sem estoque

        livro.Devolver();

        Console.WriteLine($"Quantidade final: {livro.QuantidadeDisponivel}");
    }
}