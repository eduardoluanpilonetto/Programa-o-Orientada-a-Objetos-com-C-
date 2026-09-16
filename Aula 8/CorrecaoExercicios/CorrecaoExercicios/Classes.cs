using System;
using System.Collections.Generic;

namespace ExerciciosAula8
{
    // ===================== 1. Associação Simples – Cliente e Pedido =====================
    public class Pedido
    {
        public int Numero { get; set; }
        public double Valor { get; set; }

        public Pedido(int numero, double valor)
        {
            Numero = numero;
            Valor = valor;
        }
    }

    public class Cliente
    {
        public string Nome { get; set; }
        public List<Pedido> Pedidos { get; set; } = new List<Pedido>();

        public Cliente(string nome)
        {
            Nome = nome;
        }

        public void AdicionarPedido(Pedido pedido)
        {
            Pedidos.Add(pedido);
        }

        public void ExibirPedidos()
        {
            Console.WriteLine($"Cliente: {Nome}");
            foreach (var pedido in Pedidos)
            {
                Console.WriteLine($"  Pedido #{pedido.Numero} - R$ {pedido.Valor:F2}");
            }
        }
    }

    // ===================== 2. Associação Bidirecional – Professor e Curso =====================
    public class Professor
    {
        public string Nome { get; set; }
        public List<Curso> Cursos { get; set; } = new List<Curso>();

        public Professor(string nome)
        {
            Nome = nome;
        }

        public void VincularCurso(Curso curso)
        {
            if (!Cursos.Contains(curso))
                Cursos.Add(curso);

            curso.DefinirProfessor(this);
        }
    }

    public class Curso
    {
        public string Nome { get; set; }
        public Professor ProfessorResponsavel { get; private set; }

        public Curso(string nome)
        {
            Nome = nome;
        }

        public void DefinirProfessor(Professor professor)
        {
            ProfessorResponsavel = professor;
        }
    }

    // ===================== 3. Agregação – Time e Jogador =====================
    public class Jogador
    {
        public string Nome { get; set; }

        public Jogador(string nome)
        {
            Nome = nome;
        }
    }

    public class Time
    {
        public string Nome { get; set; }
        public List<Jogador> Jogadores { get; set; } = new List<Jogador>();

        public Time(string nome)
        {
            Nome = nome;
        }

        public void AdicionarJogador(Jogador jogador)
        {
            Jogadores.Add(jogador);
        }

        public void RemoverJogador(Jogador jogador)
        {
            // Agregação: remover do time não destrói o jogador
            Jogadores.Remove(jogador);
        }
    }

    // ===================== 4. Composição – Casa e Cômodo =====================
    public class Comodo
    {
        public string Nome { get; set; }

        public Comodo(string nome)
        {
            Nome = nome;
        }
    }

    public class Casa
    {
        // Composição: a lista de cômodos é privada e só existe enquanto a casa existir
        private List<Comodo> _comodos = new List<Comodo>();

        public Casa(params string[] nomesComodos)
        {
            foreach (var nome in nomesComodos)
            {
                _comodos.Add(new Comodo(nome));
            }
        }

        public void ExibirComodos()
        {
            Console.WriteLine("Cômodos da casa:");
            foreach (var comodo in _comodos)
            {
                Console.WriteLine($"  - {comodo.Nome}");
            }
        }

        public void Destruir()
        {
            _comodos.Clear();
            Console.WriteLine("Casa destruída. Os cômodos deixaram de existir.");
        }
    }

    // ===================== 5. Dependência – Impressora e Documento =====================
    public class Documento
    {
        public string Titulo { get; set; }

        public Documento(string titulo)
        {
            Titulo = titulo;
        }
    }

    public class Impressora
    {
        // Dependência: o documento só é usado dentro do método, não é armazenado
        public void Imprimir(Documento doc)
        {
            Console.WriteLine($"Imprimindo documento: {doc.Titulo}");
        }
    }

    // ===================== 6. Sistema Escolar – Turma e Aluno =====================
    public class Aluno
    {
        public string Nome { get; set; }

        public Aluno(string nome)
        {
            Nome = nome;
        }
    }

    public class Turma
    {
        public string Nome { get; set; }
        public List<Aluno> Alunos { get; set; } = new List<Aluno>();

        public Turma(string nome)
        {
            Nome = nome;
        }

        public void AdicionarAluno(Aluno aluno)
        {
            Alunos.Add(aluno);
        }

        public void TransferirAluno(Aluno aluno, Turma novaTurma)
        {
            Alunos.Remove(aluno);
            novaTurma.AdicionarAluno(aluno);
        }

        public void ExibirAlunos()
        {
            Console.WriteLine($"Turma: {Nome}");
            foreach (var aluno in Alunos)
            {
                Console.WriteLine($"  - {aluno.Nome}");
            }
        }
    }

    // ===================== 7. Biblioteca – Livro e Autor (muitos-para-muitos) =====================
    public class Autor
    {
        public string Nome { get; set; }
        public List<Livro> Livros { get; set; } = new List<Livro>();

        public Autor(string nome)
        {
            Nome = nome;
        }
    }

    public class Livro
    {
        public string Titulo { get; set; }
        public List<Autor> Autores { get; set; } = new List<Autor>();

        public Livro(string titulo)
        {
            Titulo = titulo;
        }

        public void AdicionarAutor(Autor autor)
        {
            Autores.Add(autor);
            autor.Livros.Add(this);
        }

        public void ExibirAutores()
        {
            Console.WriteLine($"Livro: {Titulo}");
            foreach (var autor in Autores)
            {
                Console.WriteLine($"  Autor: {autor.Nome}");
            }
        }
    }

    // ===================== 8. Composição – Computador e Processador =====================
    public class Processador
    {
        public string Modelo { get; set; }
        public double ClockGHz { get; set; }

        public Processador(string modelo, double clockGHz)
        {
            Modelo = modelo;
            ClockGHz = clockGHz;
        }
    }

    public class Computador
    {
        public string Marca { get; set; }

        // Composição: o processador é criado junto com o computador e não existe fora dele
        private Processador _processador;

        public Computador(string marca, string modeloProcessador, double clockGHz)
        {
            Marca = marca;
            _processador = new Processador(modeloProcessador, clockGHz);
        }

        public void ExibirInformacoes()
        {
            Console.WriteLine($"Computador: {Marca}");
            Console.WriteLine($"  Processador: {_processador.Modelo} ({_processador.ClockGHz} GHz)");
        }
    }

    // ===================== 9. Dependência – Serviço de E-mail =====================
    public class Usuario
    {
        public string Nome { get; set; }
        public string Email { get; set; }

        public Usuario(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }
    }

    public class ServicoEmail
    {
        // Dependência: Usuario é usado apenas durante o envio, não é armazenado na classe
        public void EnviarEmail(Usuario usuario)
        {
            Console.WriteLine($"Enviando e-mail para {usuario.Nome} ({usuario.Email})");
        }
    }

    // ===================== 10. Projeto Completo – Empresa =====================
    public class Funcionario
    {
        public string Nome { get; set; }

        public Funcionario(string nome)
        {
            Nome = nome;
        }
    }

    public class Departamento
    {
        public string Nome { get; set; }
        public List<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();

        public Departamento(string nome)
        {
            Nome = nome;
        }

        public void AdicionarFuncionario(Funcionario funcionario)
        {
            Funcionarios.Add(funcionario);
        }

        public void MudarFuncionario(Funcionario funcionario, Departamento novoDepartamento)
        {
            Funcionarios.Remove(funcionario);
            novoDepartamento.AdicionarFuncionario(funcionario);
        }
    }

    public class Empresa
    {
        public string Nome { get; set; }
        public List<Departamento> Departamentos { get; set; } = new List<Departamento>();

        public Empresa(string nome)
        {
            Nome = nome;
        }

        public void AdicionarDepartamento(Departamento departamento)
        {
            Departamentos.Add(departamento);
        }

        public void ExibirEstrutura()
        {
            Console.WriteLine($"Empresa: {Nome}");
            foreach (var dep in Departamentos)
            {
                Console.WriteLine($"  Departamento: {dep.Nome}");
                foreach (var func in dep.Funcionarios)
                {
                    Console.WriteLine($"    Funcionário: {func.Nome}");
                }
            }
        }
    }
}