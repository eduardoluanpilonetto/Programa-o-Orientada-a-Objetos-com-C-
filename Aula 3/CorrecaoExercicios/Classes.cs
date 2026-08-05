namespace Classes
{
    internal class Pessoa
    {
        public string nome;
        private int idade;

        public int GetIdade()
        {
            return idade;
        }

        public void SetIdade(int valor)
        {
            if (valor > 0) idade = valor;
            else Console.WriteLine("Idade Inválida");
        }
    }

    class Produto
    {
        public string nome;
        private double preco;
        public double Preco
        {
            get { return preco; }
            set
            {
                if (value > 0) preco = value;
                else Console.WriteLine("Valor Invalido");
            }
        }

        public void AplicarDesconto(double valor)
        {
            if (valor < preco) preco = preco - valor;
            else Console.WriteLine("Valor Invalido");
        }
    }

    class ContaBancaria
    {
        public string titular;
        private double saldo = 0;
        public double Saldo
        {
            get { return saldo; }
            private set { saldo = value; }
        }

        public void Depositar(double valor)
        {
            if(valor <= 0)
            {
                Console.WriteLine("Valor Inválido");
            }
            else
            {
                saldo += valor;
            }
        }

        public void Sacar(double valor)
        {
            if(valor > saldo)
            {
                Console.WriteLine("Saque Indisponível");
            }
            else
            {
                saldo = saldo - valor;
            }
        }
    }

    public class Carro
    {
        private int velocidade;
        public int Velocidade
        {
            get { return velocidade; }
        }

        public void Acelerar(int valor)
        {
            if (valor > 0)
            {
                velocidade += valor;

                if (velocidade > 220)
                {
                    velocidade = 220;
                }
            }
        }

        // Método para frear
        public void Frear(int valor)
        {
            if (valor > 0)
            {
                velocidade -= valor;

                if (velocidade < 0)
                {
                    velocidade = 0;
                }
            }
        }
    }

    class Aluno
    {
        public string Nome;
        private double nota1;
        private double nota2;

        public void DefinirNotas(double n1, double n2)
        {
            if (n1 >= 0 && n1 <= 10)
                nota1 = n1;
            else
                Console.WriteLine("A nota 1 deve estar entre 0 e 10.");

            if (n2 >= 0 && n2 <= 10)
                nota2 = n2;
            else
                Console.WriteLine("A nota 2 deve estar entre 0 e 10.");
        }

        public double CalcularMedia()
        {
            return (nota1 + nota2) / 2;
        }

        public bool VerificarAprovacao()
        {
            return CalcularMedia() >= 7.0;
        }
    }

    class Funcionario
    {
        public string Nome;
        private double salario;

        public double Salario
        {
            get { return salario; }
            set
            {
                if (value > 0) salario = value;
                else Console.WriteLine("Salário inválido");
            }
        }

        public void AumentarSlario(double aumento)
        {
            if(aumento < 0 || aumento > 100)
            {
                Console.WriteLine($"Porcentagem inválida");
            } else
            {
                salario = salario * (1 + (aumento / 100));
                Console.WriteLine($"Novo Salário {salario}");
            }

        }
    }

    class User
    { 
        public string Login { get; set; }
        private string password;

        private bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            } else if (password.Length < 6)
            {
                return false;
            }

            return true;
        }

        public string Password
        {
            get { return password;  }
            set
            {
                if (!ValidatePassword(value)) return;

                password = value;
            }
        }

        public void AlterarSenha(string newPassword)
        {
            if (!ValidatePassword(newPassword)) return;
        }

        public void ExecuteLogin(string informedLogin, string informedPassword)
        {
            if (Login == informedLogin && password == informedPassword)
            {
                Console.WriteLine("login ta feito");
                return;
            }

            Console.WriteLine("login ta errado");
        }
    }

    class Estoque
    {
        private int quantidade = 0;
        public int Quantidade
        {
            get { return quantidade; }
            private set { quantidade = value; }
        }

        public void AdicionaQtd(int valor)
        {
            if(valor > 0)
            {
                quantidade = valor;
            }
            else
            {
                Console.WriteLine("Operação negada, valor inválido");
            }
        }

        public void RemoveQtd(int valor)
        {
            if((quantidade - valor) < 0)
            {
                Console.WriteLine("Operação negada, valor inválido");
            }else
            {
                quantidade -= valor;
            }
        }
    }

    public class Retangulo
    {
        private double largura;
        private double altura;
        public double Largura
        {
            get { return largura; }
            set
            {
                if (value <= 0)
                    Console.WriteLine("A largura deve ser maior que zero.");
                largura = value;
            }
        }
        public double Altura
        {
            get { return altura; }
            set
            {
                if (value <= 0)
                    Console.WriteLine("A altura deve ser maior que zero.");
                altura = value;
            }
        }

        public double CalcularArea()
        {
            return Largura * Altura;
        }
        public double CalcularPerimetro()
        {
            return 2 * (Largura + Altura);
        }
    }

public class Livro
    {
        public string Titulo { get; set; }
        private int quantidadeDisponivel;
        public int QuantidadeDisponivel
        {
            get { return quantidadeDisponivel; }
            set
            {
                if (value <= 0)
                    Console.WriteLine("A Quantidade deve ser maior que zero.");
                quantidadeDisponivel = value;
            }
        }

        public void Emprestar()
        {
            if (quantidadeDisponivel <= 0)
            {
                Console.WriteLine($"Não é possível emprestar '{Titulo}': sem estoque disponível.");
                return;
            }

            quantidadeDisponivel--;
            Console.WriteLine($"Livro '{Titulo}' emprestado. Restam {quantidadeDisponivel} unidade(s).");
        }

        public void Devolver()
        {
            quantidadeDisponivel++;
            Console.WriteLine($"Livro '{Titulo}' devolvido. Agora há {quantidadeDisponivel} unidade(s) disponível(is).");
        }
    }
}
