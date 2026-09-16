using System;
using System.Timers;

namespace ExerciciosAula8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== 1. Associação Simples – Cliente e Pedido =====");
            Cliente cliente = new Cliente("Maria Silva");
            cliente.AdicionarPedido(new Pedido(1, 150.90));
            cliente.AdicionarPedido(new Pedido(2, 89.50));
            cliente.ExibirPedidos();

            Console.WriteLine("\n===== 2. Associação Bidirecional – Professor e Curso =====");
            Professor professor = new Professor("João Souza");
            Curso curso1 = new Curso("Programação C#");
            Curso curso2 = new Curso("Banco de Dados");
            professor.VincularCurso(curso1);
            professor.VincularCurso(curso2);
            Console.WriteLine($"{curso1.Nome} - Professor responsável: {curso1.ProfessorResponsavel.Nome}");
            Console.WriteLine($"{curso2.Nome} - Professor responsável: {curso2.ProfessorResponsavel.Nome}");

            Console.WriteLine("\n===== 3. Agregação – Time e Jogador =====");
            Time time = new Time("Tigres FC");
            Jogador jogador1 = new Jogador("Carlos");
            Jogador jogador2 = new Jogador("Pedro");
            time.AdicionarJogador(jogador1);
            time.AdicionarJogador(jogador2);
            Console.WriteLine($"Jogadores no {time.Nome}: {time.Jogadores.Count}");
            time.RemoverJogador(jogador1);
            Console.WriteLine($"Após remoção: {time.Jogadores.Count} jogador(es) no time.");
            Console.WriteLine($"O jogador {jogador1.Nome} continua existindo fora do time (agregação).");

            Console.WriteLine("\n===== 4. Composição – Casa e Cômodo =====");
            Casa casa = new Casa("Sala", "Cozinha", "Quarto");
            casa.ExibirComodos();
            casa.Destruir();

            Console.WriteLine("\n===== 5. Dependência – Impressora e Documento =====");
            Impressora impressora = new Impressora();
            Documento documento = new Documento("Relatório Mensal");
            impressora.Imprimir(documento);

            Console.WriteLine("\n===== 6. Sistema Escolar – Turma e Aluno =====");
            Turma turmaA = new Turma("Turma A");
            Turma turmaB = new Turma("Turma B");
            Aluno aluno1 = new Aluno("Ana");
            Aluno aluno2 = new Aluno("Bruno");
            turmaA.AdicionarAluno(aluno1);
            turmaA.AdicionarAluno(aluno2);
            turmaA.ExibirAlunos();
            turmaA.TransferirAluno(aluno1, turmaB);
            Console.WriteLine("Após transferência:");
            turmaA.ExibirAlunos();
            turmaB.ExibirAlunos();

            Console.WriteLine("\n===== 7. Biblioteca – Livro e Autor =====");
            Livro livro = new Livro("Padrões de Projeto em C#");
            Autor autor1 = new Autor("Fernanda Lima");
            Autor autor2 = new Autor("Ricardo Alves");
            livro.AdicionarAutor(autor1);
            livro.AdicionarAutor(autor2);
            livro.ExibirAutores();

            Console.WriteLine("\n===== 8. Composição – Computador e Processador =====");
            Computador computador = new Computador("Dell", "Intel i7", 3.6);
            computador.ExibirInformacoes();

            Console.WriteLine("\n===== 9. Dependência – Serviço de E-mail =====");
            Usuario usuario = new Usuario("Lucas", "lucas@email.com");
            ServicoEmail servicoEmail = new ServicoEmail();
            servicoEmail.EnviarEmail(usuario);

            Console.WriteLine("\n===== 10. Projeto Completo – Empresa =====");
            Empresa empresa = new Empresa("Tech Solutions");
            Departamento ti = new Departamento("TI");
            Departamento rh = new Departamento("RH");
            Funcionario funcionario1 = new Funcionario("Paula");
            Funcionario funcionario2 = new Funcionario("Marcos");
            ti.AdicionarFuncionario(funcionario1);
            rh.AdicionarFuncionario(funcionario2);
            empresa.AdicionarDepartamento(ti);
            empresa.AdicionarDepartamento(rh);
            empresa.ExibirEstrutura();

            Console.WriteLine("\nMudando funcionário de departamento...");
            ti.MudarFuncionario(funcionario1, rh);
            empresa.ExibirEstrutura();

            Console.WriteLine("\nPressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}