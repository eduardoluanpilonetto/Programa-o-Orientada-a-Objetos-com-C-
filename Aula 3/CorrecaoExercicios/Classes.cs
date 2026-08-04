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
        private double saldo;
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
                saldo = valor;
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
}
