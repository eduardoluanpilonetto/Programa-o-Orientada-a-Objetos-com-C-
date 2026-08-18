using System;

namespace HerancaExercicios
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== Exercício 1 - Classe Animal =====");
            Animal animalGenerico = new Animal("Bicho");
            animalGenerico.Comer();
            animalGenerico.Dormir();
            Console.WriteLine();

            Console.WriteLine("===== Exercício 2 - Classe Cachorro =====");
            Cachorro rex = new Cachorro("Rex");
            rex.Comer();
            rex.Dormir();
            rex.Latir();
            Console.WriteLine();

            Console.WriteLine("===== Exercício 3 - Classe Gato =====");
            Gato mimi = new Gato("Mimi");
            mimi.Comer();   // reutilização de método herdado
            mimi.Dormir();  // reutilização de método herdado
            mimi.Miar();
            Console.WriteLine();

            Console.WriteLine("===== Exercício 4 - Veiculo, Carro e Moto =====");
            Carro carro = new Carro("Fusca");
            carro.Ligar();
            carro.AbrirPortaMalas();
            carro.Desligar();

            Moto moto = new Moto("Honda CB500");
            moto.Ligar();
            moto.EmpinarRoda();
            moto.Desligar();
            Console.WriteLine();

            Console.WriteLine("===== Exercício 5 - Sobrescrita de EmitirSom =====");
            Animal[] animais = { rex, mimi };
            foreach (Animal a in animais)
            {
                a.EmitirSom();
            }
            Console.WriteLine();

            Console.WriteLine("===== Exercício 6 - Pessoa, Aluno e Professor =====");
            Aluno aluno = new Aluno("Lucas", 20, "Análise e Desenvolvimento de Sistemas");
            aluno.Apresentar();
            aluno.Estudar();

            Professor professor = new Professor("Eduardo", 25, "Programação Orientada a Objetos");
            professor.Apresentar();
            professor.Lecionar();
            Console.WriteLine();

            Console.WriteLine("===== Exercício 7 - Funcionario, Gerente e Vendedor =====");
            Gerente gerente = new Gerente("Carlos");
            gerente.BaterPonto();
            gerente.GerenciarEquipe();

            Vendedor vendedor = new Vendedor("Marina");
            vendedor.BaterPonto();
            vendedor.RealizarVenda();
            Console.WriteLine();

            Console.WriteLine("===== Exercício 8 - Produto, Notebook e Celular =====");
            Notebook notebook = new Notebook("Dell Inspiron", 3500.00, "Intel i5");
            notebook.ExibirInformacoes();
            notebook.ExibirProcessador();

            Celular celular = new Celular("Samsung Galaxy", 2200.00, 128);
            celular.ExibirInformacoes();
            celular.ExibirArmazenamento();
            Console.WriteLine();

            Console.WriteLine("===== Exercício 9 - Personagem, Guerreiro e Mago =====");
            Personagem[] personagens =
            {
                new Guerreiro("Thor"),
                new Mago("Merlin")
            };

            foreach (Personagem p in personagens)
            {
                p.Atacar();   // polimorfismo: cada um ataca de um jeito
                p.Defender(); // reutilização do método herdado
            }
            Console.WriteLine();

            Console.WriteLine("===== Exercício 10 - Desafio Completo (Cachorro, Gato, Passaro) =====");
            Animal[] sistemaAnimais =
            {
                new Cachorro("Bidu"),
                new Gato("Garfield"),
                new Passaro("Piu")
            };

            foreach (Animal a in sistemaAnimais)
            {
                a.Comer();       // reutilização
                a.Dormir();      // reutilização
                a.EmitirSom();   // sobrescrita (polimorfismo)

                // Métodos específicos de cada classe filha
                if (a is Cachorro c) c.Latir();
                if (a is Gato g) g.Miar();
                if (a is Passaro pa) pa.Voar();

                Console.WriteLine();
            }

            Console.WriteLine("Fim do programa. Pressione qualquer tecla para sair.");
            Console.ReadKey();
        }
    }
}