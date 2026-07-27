Produto P1 = new Produto();
P1.Nome = "Sabonete";
P1.Preco = 2.99;
P1.Quantidade = 10;
P1.Ncm = "00012345";
P1.Apresentar();

class Produto
{
    public string Nome;
    public double Preco;
    public int Quantidade;
    public string Ncm;

    public void Apresentar()
    {
        Console.WriteLine($"Nome: {Nome}; Preco:{Preco}; Quantidade: {Quantidade}; NCM:{Ncm}");
    }
}