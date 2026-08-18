using System;

namespace HerancaExercicios
{
    // ============================================================
    // EXERCÍCIOS 1, 2, 3, 5 e 10 - Animal, Cachorro, Gato, Passaro
    // ============================================================
    public class Animal
    {
        public string Nome { get; set; }

        public Animal(string nome)
        {
            Nome = nome;
        }

        public void Comer()
        {
            Console.WriteLine($"{Nome} está comendo.");
        }

        public void Dormir()
        {
            Console.WriteLine($"{Nome} está dormindo.");
        }

        // Exercício 5: método virtual para ser sobrescrito
        public virtual void EmitirSom()
        {
            Console.WriteLine("Som genérico de animal.");
        }
    }

    public class Cachorro : Animal
    {
        public Cachorro(string nome) : base(nome) { }

        public void Latir()
        {
            Console.WriteLine($"{Nome} está latindo: Au au!");
        }

        public override void EmitirSom()
        {
            Console.WriteLine($"{Nome} (Cachorro) diz: Au au!");
        }
    }

    public class Gato : Animal
    {
        public Gato(string nome) : base(nome) { }

        public void Miar()
        {
            Console.WriteLine($"{Nome} está miando: Miau!");
        }

        public override void EmitirSom()
        {
            Console.WriteLine($"{Nome} (Gato) diz: Miau!");
        }
    }

    // Exercício 10: classe adicional Passaro
    public class Passaro : Animal
    {
        public Passaro(string nome) : base(nome) { }

        public void Voar()
        {
            Console.WriteLine($"{Nome} está voando pelo céu.");
        }

        public override void EmitirSom()
        {
            Console.WriteLine($"{Nome} (Pássaro) diz: Piu piu!");
        }
    }

    // ============================================================
    // EXERCÍCIO 4 - Veiculo, Carro, Moto
    // ============================================================
    public class Veiculo
    {
        public string Modelo { get; set; }

        public Veiculo(string modelo)
        {
            Modelo = modelo;
        }

        public void Ligar()
        {
            Console.WriteLine($"{Modelo} está ligado.");
        }

        public void Desligar()
        {
            Console.WriteLine($"{Modelo} está desligado.");
        }
    }

    public class Carro : Veiculo
    {
        public Carro(string modelo) : base(modelo) { }

        public void AbrirPortaMalas()
        {
            Console.WriteLine($"{Modelo}: porta-malas aberto.");
        }
    }

    public class Moto : Veiculo
    {
        public Moto(string modelo) : base(modelo) { }

        public void EmpinarRoda()
        {
            Console.WriteLine($"{Modelo}: empinando a roda!");
        }
    }

    // ============================================================
    // EXERCÍCIO 6 - Pessoa, Aluno, Professor
    // ============================================================
    public class Pessoa
    {
        public string Nome { get; set; }
        public int Idade { get; set; }

        public Pessoa(string nome, int idade)
        {
            Nome = nome;
            Idade = idade;
        }

        public void Apresentar()
        {
            Console.WriteLine($"Olá, meu nome é {Nome} e tenho {Idade} anos.");
        }
    }

    public class Aluno : Pessoa
    {
        public string Curso { get; set; }

        public Aluno(string nome, int idade, string curso) : base(nome, idade)
        {
            Curso = curso;
        }

        public void Estudar()
        {
            Console.WriteLine($"{Nome} está estudando o curso de {Curso}.");
        }
    }

    public class Professor : Pessoa
    {
        public string Disciplina { get; set; }

        public Professor(string nome, int idade, string disciplina) : base(nome, idade)
        {
            Disciplina = disciplina;
        }

        public void Lecionar()
        {
            Console.WriteLine($"{Nome} está lecionando a disciplina de {Disciplina}.");
        }
    }

    // ============================================================
    // EXERCÍCIO 7 - Funcionario, Gerente, Vendedor
    // ============================================================
    public class Funcionario
    {
        public string Nome { get; set; }

        public Funcionario(string nome)
        {
            Nome = nome;
        }

        public void BaterPonto()
        {
            Console.WriteLine($"{Nome} bateu o ponto.");
        }
    }

    public class Gerente : Funcionario
    {
        public Gerente(string nome) : base(nome) { }

        public void GerenciarEquipe()
        {
            Console.WriteLine($"{Nome} está gerenciando a equipe.");
        }
    }

    public class Vendedor : Funcionario
    {
        public Vendedor(string nome) : base(nome) { }

        public void RealizarVenda()
        {
            Console.WriteLine($"{Nome} realizou uma venda.");
        }
    }

    // ============================================================
    // EXERCÍCIO 8 - Produto, Notebook, Celular
    // ============================================================
    public class Produto
    {
        public string Nome { get; set; }
        public double Preco { get; set; }

        public Produto(string nome, double preco)
        {
            Nome = nome;
            Preco = preco;
        }

        public void ExibirInformacoes()
        {
            Console.WriteLine($"Produto: {Nome} - R$ {Preco:F2}");
        }
    }

    public class Notebook : Produto
    {
        public string Processador { get; set; }

        public Notebook(string nome, double preco, string processador) : base(nome, preco)
        {
            Processador = processador;
        }

        public void ExibirProcessador()
        {
            Console.WriteLine($"{Nome} possui processador {Processador}.");
        }
    }

    public class Celular : Produto
    {
        public int Armazenamento { get; set; }

        public Celular(string nome, double preco, int armazenamento) : base(nome, preco)
        {
            Armazenamento = armazenamento;
        }

        public void ExibirArmazenamento()
        {
            Console.WriteLine($"{Nome} possui {Armazenamento}GB de armazenamento.");
        }
    }

    // ============================================================
    // EXERCÍCIO 9 - Personagem, Guerreiro, Mago
    // ============================================================
    public class Personagem
    {
        public string Nome { get; set; }

        public Personagem(string nome)
        {
            Nome = nome;
        }

        public virtual void Atacar()
        {
            Console.WriteLine($"{Nome} ataca de forma genérica.");
        }

        public void Defender()
        {
            Console.WriteLine($"{Nome} está se defendendo.");
        }
    }

    public class Guerreiro : Personagem
    {
        public Guerreiro(string nome) : base(nome) { }

        public override void Atacar()
        {
            Console.WriteLine($"{Nome} ataca com a espada!");
        }
    }

    public class Mago : Personagem
    {
        public Mago(string nome) : base(nome) { }

        public override void Atacar()
        {
            Console.WriteLine($"{Nome} ataca com uma bola de fogo!");
        }
    }
}