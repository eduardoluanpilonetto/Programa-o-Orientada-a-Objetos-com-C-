using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrecaoExercicios
{
    //EXERCICIO 1
    class Pessoa
    {
        public string Nome;
        public int Idade;
        public void Apresentar()
        {
            Console.WriteLine($"Olá, meu nome é {Nome} e tenho {Idade} anos.");
        }
    }

    //EXERCICIO 2
    class Carro
    {
        public string Modelo;
        public int Velocidade = 0;
        public void Acelerar()
        {
            Velocidade += 10;
        }

        public void Frear()
        {
            Velocidade -= 10;
        }

        public void VerVelocidade()
        {
            Console.WriteLine($"Velocidade Atual: {Velocidade}");
        }
    }
    //ECERCÍCIO 3
    class Produto
    {
        public string Nome;
        public int Quantidade;
        public string Preco;
        
        public void AdicionarQtd(int qtd) 
        {
            Quantidade = Quantidade + qtd;
        }
        public void RemoverQtd(int qtd)
        {
            Quantidade = Quantidade - qtd;
        }
        public int RetornaQtd()
        {
            return Quantidade;
        }
    }

    //EXERCICIO 4
    class Aluno
    {
        public string Nome;
        public double Nota;
        public void VerificarSituacao()
        {
            if (Nota < 5) Console.WriteLine("Reprovado");
            else if (Nota < 7) Console.WriteLine("Em Recuperação");
            else Console.WriteLine("Aprovado");
        }
    }

    //EXERCÍCIO 5
    class ContaBancaria
    {
        public string Titular;
        public double Saldo = 0;
        public void Depositar(double Valor)
        {
            Saldo += Valor;
        }
        public void Sacar(double Valor)
        {
            if(Saldo <= 0 || Saldo - Valor < 0)
            {
                Console.WriteLine("Saque indisponível");
                return;
            }
            Saldo -= Valor;
        }
        public void MostrarSaldo()
        {
            Console.WriteLine($"Saldo Atual : {Saldo}");
        }
    }
    //EXERCICIO 6
    class Animal
    {
        public string Nome;
        public string Som;
        public string Especie;
        public void EmitirSom()
        {
            Console.WriteLine(Som);
        }
    }
    //EXERCICIO 7
    class Funcionario
    {
        public string Nome;
        public double Salario;
        public void AumentarSalario(double percentual)
        {
            Salario = Salario * (1 + (percentual / 100));
            Console.WriteLine($"Novo Salário {Salario}");
        }
    }

    //EXERCÍCIO 9
    class Retangulo
    {
        public double Largura;
        public double Autura;
        public void CalcularArea()
        {
            Console.WriteLine($"A área do Retangulo é {Largura * Autura}");
        }
        public void CalcularPerimetro()
        {
            Console.WriteLine($"O perimetro do Retangulo é {(Largura * 2) + (Autura * 2)}");
        }
    }

    //ECERCÍCIO 10
    class Livro
    {
        public string Autor;
        public string Titulo;
        public bool Disponivel;
        public void LivroEmprestado()
        {
            if (Disponivel)
            {
                Console.WriteLine("Livro não Emprestado");
            }
            else
            {
                Console.WriteLine("Livro Emprestado");
            }
        }

    }
}
