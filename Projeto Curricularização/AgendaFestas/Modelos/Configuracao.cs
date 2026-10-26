namespace AgendaFestas.Modelos;

/// <summary>Dados da empresa que aparecem no cabeçalho dos PDFs.</summary>
public class Configuracao
{
    public string NomeEmpresa { get; set; } = "Espaço de Festas";
    public string Telefone { get; set; } = "";
    public string Endereco { get; set; } = "";
    public string ObservacaoCatalogo { get; set; } = "Valores sujeitos a alteração sem aviso prévio. Consulte disponibilidade de datas.";
}
