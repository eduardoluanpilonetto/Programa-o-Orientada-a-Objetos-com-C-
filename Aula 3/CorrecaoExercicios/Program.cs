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
    }
}