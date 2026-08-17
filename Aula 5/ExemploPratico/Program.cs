class Program
{
    public static void Main(string[] args)
    {
        Cachorro meuDoguinho = new Cachorro();
        meuDoguinho.Nome = "Eduardo";
        meuDoguinho.Comer();
        meuDoguinho.EmitirSom();

        Gato meuFelino = new Gato();
        meuFelino.Nome = "Mimi";
        meuFelino.Comer();
        meuFelino.EmitirSom();
    }
}

public class Animal
{
    public string Nome;

    public virtual void EmitirSom()
    {
        Console.WriteLine("Som do animal");
    }

    public void Comer()
    {
        Console.WriteLine("Comendo...");
    }
}

public class Cachorro : Animal
{
    public override void EmitirSom()
    {
        Console.WriteLine("Au Au!");
    }
}

public class Gato : Animal
{
    public override void EmitirSom()
    {
        Console.WriteLine("Miau!");
    }
}
