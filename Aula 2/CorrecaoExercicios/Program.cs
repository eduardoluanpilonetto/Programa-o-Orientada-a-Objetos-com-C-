using Classes;

class Program
{
    public static void Main(string[] args)
    {
        Pessoa pessoa = new Pessoa();
        pessoa.Nome = "Carlos";
        pessoa.Cidade = "São paulo";
        pessoa.Idade = 25;

        pessoa.Apresentar();


        Produto produto1 = new Produto();
        produto1.Nome = "Cerveja Skol lata 273 Ml";
        produto1.Quantidade = 12;
        produto1.Preco = 3.99;


        produto1.MostrarProduto();

        Carro velho = new Carro();
        velho.Modelo = "Kaddet";
        velho.Marca = "Volkswagen";
        velho.Ano = 1980;
        velho.Cor = "Vermelho";

        velho.MostrarCarro();

        Aluno aluno = new Aluno();
        aluno.Nome = "Vinicius";
        aluno.Curso = "Eng Software";
        aluno.Nota01 = 5.3;
        aluno.Nota02 = 9.4;
        aluno.CalcularMedia();

        ContaBancaria Conta1 = new ContaBancaria();

        Conta1.titular = "João";
        Conta1.saldo = 0;
        Conta1.Deposito(900);
        Conta1.Sacar(100);
        Conta1.MostrarSaldo();

        Funcionario P6 = new Funcionario();
        P6.Nome = "Carlos";
        P6.Cargo = "Analista";
        P6.Salario = 3000;
        P6.AumentarSalario(10);

        Telefone telefone = new Telefone();
        telefone.Marca = "Xiomi";
        telefone.Modelo = "Redmi Note 8";
        telefone.Bateria = 85;

        telefone.Ligar();
        telefone.MistrarBateria();
        telefone.Desligar();
    }
}
