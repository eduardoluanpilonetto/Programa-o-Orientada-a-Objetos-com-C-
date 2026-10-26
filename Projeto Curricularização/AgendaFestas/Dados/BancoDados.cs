using AgendaFestas.Modelos;

namespace AgendaFestas.Dados;

/// <summary>Ponto único de acesso aos arquivos de dados do sistema.</summary>
public class BancoDados
{
    private readonly string _arquivoConfiguracao;

    public string Pasta { get; }
    public IRepositorio<Cliente> Clientes { get; }
    public IRepositorio<TipoServico> TiposServico { get; }
    public IRepositorio<Agendamento> Agendamentos { get; }

    public BancoDados(string pasta)
    {
        Pasta = pasta;
        Directory.CreateDirectory(pasta);
        Clientes = new RepositorioJson<Cliente>(Path.Combine(pasta, "clientes.json"));
        TiposServico = new RepositorioJson<TipoServico>(Path.Combine(pasta, "servicos.json"));
        Agendamentos = new RepositorioJson<Agendamento>(Path.Combine(pasta, "agendamentos.json"));
        _arquivoConfiguracao = Path.Combine(pasta, "configuracao.json");
    }

    public Configuracao ObterConfiguracao() =>
        ArquivoJson.Ler<Configuracao>(_arquivoConfiguracao) ?? new Configuracao();

    public void SalvarConfiguracao(Configuracao configuracao) =>
        ArquivoJson.Gravar(_arquivoConfiguracao, configuracao);
}
