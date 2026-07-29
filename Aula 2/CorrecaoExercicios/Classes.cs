using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    class Pessoa
    {
        public string Nome;
        public string Cidade;
        public int Idade;

        public void Apresentar()
        {
            Console.WriteLine($"Nome: {Nome}\nCidade: {Cidade}\nIdade: {Idade}");
        }
    }

    class Produto
    {
        public string Nome;
        public int Quantidade;
        public double Preco;

        public void MostrarProduto()
        {
            Console.WriteLine($"Nome: {Nome}\nQuantidade {Quantidade}\npreço {Preco}");
        }
    }

    class Carro
    {
        public string Modelo;
        public string Marca;
        public int Ano;
        public string Cor;

        public void MostrarCarro()
        {
            Console.WriteLine($"Meu carro {Cor} é um {Modelo} da marca {Marca} ano {Ano}");
        }
    }

    class Aluno
    {
        public string? Nome;
        public string? Curso;
        public double Nota01;
        public double Nota02;

        public void CalcularMedia()
        {
            double notaFinal = (Nota01 + Nota02) / 2;
            Console.WriteLine($"O aluno {Nome} do curso {Curso} ficou com a média final {notaFinal}");
        }
    }
    class ContaBancaria
    {
        public string titular;
        public double saldo;

        public void Deposito(double valor)
        {
            saldo = saldo + valor;
        }

        public void Sacar(double valor)
        {
            if(valor > saldo)
            {
                Console.Write("Saque Indisponível");
                return;
            }
            saldo = saldo - valor;
        }

        public void MostrarSaldo()
        {
            Console.WriteLine($"O valor do saldo é de: {saldo}");
        }

    }

    class Funcionario
    {
        public string Nome;
        public string Cargo;
        public double Salario;
        public void Apresentar()
        {
            Console.WriteLine($"Nome: {Nome}; Cargo: {Cargo}; Salario: {Salario}");
        }
        public void AumentarSalario(double porcentagem)
        {
            Salario = ((porcentagem / 100) + 1) * Salario;
            Console.WriteLine($"Salario aumentado em {porcentagem}% e passou a ser R${Salario}");
        }
    }

    public class Telefone
    {

        public string Marca;
        public string Modelo;
        public int Bateria;
        public bool Ligado;

        public void MistrarBateria()
        {
            Console.WriteLine($"Bateria: {Bateria}%");
        }

        public void Ligar()
        {
            Ligado = true;
        }

        public void Desligar()
        {
            Ligado = false;
        }
    }

    class Livro
    {
        public string Titulo;
        public string Autor;
        public int QtdPagina;

        public void ExibirLivro()
        {
            Console.WriteLine($"Livro {Titulo} | Autor {Autor} | Qtd Páginas {QtdPagina}");
        }
    }

    class Retangulo
    {
        public double Base;
        public double Altura;

        public void CalcularArea()
        {
            Console.WriteLine($"Área: {Base * Altura}cm2");
        }
        public void CalcularPerimetro()
        {
            Console.WriteLine($"Perímetro: {(Base + Altura) * 2}");
        }
    }

    class Pet
    {
        public string Nome;
        public string Tipo;
        public int Idade;
        public double Peso;

        public void ExibirPet()
        {
            Console.WriteLine($"Meu pet tem o nome de {Nome}, o tipo do pet é {Tipo},a idade é {Idade} e o peso é {Peso}");
        }
    }

}