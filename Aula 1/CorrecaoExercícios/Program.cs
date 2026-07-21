using CorrecaoExercicios;
using System;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("INSTANCA EXERCICIO 1");
        Pessoa pessoa = new Pessoa();
        pessoa.Nome = "Eduardo";
        pessoa.Idade = 25;
        pessoa.Apresentar();

        Console.WriteLine("INSTANCA EXERCICIO 2");
        Carro carro = new Carro();
        carro.Modelo = "Opela";
        carro.Acelerar();
        carro.Acelerar();
        carro.Frear();
        carro.VerVelocidade();

        Console.WriteLine("INSTANCA EXERCICIO 3");
        Produto prod = new Produto();
        prod.Nome = "Sabonete";
        prod.AdicionarQtd(114);
        prod.RemoverQtd(5);
        prod.RemoverQtd(5);
        Console.WriteLine($"Quantidade Atual: {prod.RetornaQtd()}");

        Console.WriteLine("INSTANCA EXERCICIO 4");
        Aluno alu = new Aluno();
        alu.Nome = "Frederico";
        alu.Nota = 6;
        alu.VerificarSituacao();

        Console.WriteLine("INSTANCA EXERCICIO 5");
        ContaBancaria conta = new ContaBancaria();
        conta.Titular = "Eduardo";
        conta.Sacar(10);
        conta.Depositar(20);
        conta.Sacar(10);
        conta.MostrarSaldo();

        Console.WriteLine("INSTANCA EXERCICIO 6");
        Animal animal = new Animal();
        animal.Nome = "Javali";
        animal.Som = "UiUiUi";
        animal.Especie = "Argentino";
        animal.EmitirSom();

        Console.WriteLine("INSTANCA EXERCICIO 7");
        Funcionario func = new Funcionario();
        func.Nome = "Marco";
        func.Salario = 1000;
        func.AumentarSalario(14);

        //Qual ficou mais organizado? O feito com POO
        //Qual ficou mais fácil de entender? POO foi o mais fácil

        Console.WriteLine("INSTANCA EXERCICIO 9");
        Retangulo ret = new Retangulo();
        ret.Largura = 3;
        ret.Autura = 2;
        ret.CalcularPerimetro();
        ret.CalcularArea();

        Console.WriteLine("INSTANCA EXERCICIO 10");
        Livro liv = new Livro();
        liv.Titulo = "A volta dos que não foram";
        liv.Autor = "O autor";
        liv.Disponivel = true;
        liv.LivroEmprestado();
    }
}
