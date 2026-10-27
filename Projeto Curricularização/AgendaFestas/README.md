# Agenda de Festas

Sistema de agendamentos para espaço de festas em C# (Windows Forms, .NET 9), com dados em arquivos JSON e geração de PDFs.

## Como gerar o .exe
Dê dois cliques em `publicar.bat`. Ele cria a pasta `publicado` com um único `AgendaFestas.exe`, que roda sem instalar nada.
Copie essa pasta para o computador do cliente. Os dados ficam em `publicado\dados\*.json`. Para fazer backup, basta copiar a pasta `dados`.

## Estrutura (camadas)
| Pasta | Responsabilidade |
|---|---|
| `Modelos/` | Entidades: `Cliente`, `TipoServico`, `VariacaoServico` (preço por dia da semana), `Agendamento`, `ItemAgendamento`. Todas herdam de `Entidade`, que exige o método `Validar()` |
| `Dados/` | "Banco" em JSON: interface `IRepositorio<T>` e implementação genérica `RepositorioJson<T>` |
| `Servicos/` | Regras de negócio (cliente com agendamento não pode ser excluído, conflito de data, recálculo de preços) e montagem dos relatórios |
| `Pdf/` | `DocumentoPdf` (classe abstrata com cabeçalho e rodapé) e as subclasses `CatalogoPdf`, `RelatorioAgendamentosPdf`, `RelatorioCaixaPdf` |
| `Telas/` | Formulários. Todos herdam de `FormBase` |

## Conceitos de POO usados
- **Herança e abstração:** `Entidade`, `DocumentoPdf`, `FormBase`
- **Polimorfismo:** cada entidade implementa o próprio `Validar()`, e cada PDF implementa o próprio `ComporConteudo()`
- **Interface e genéricos:** `IRepositorio<T>` / `RepositorioJson<T>`
- **Encapsulamento:** propriedades calculadas (`ValorTotal`, `Saldo`, `Subtotal`) e o repositório devolve cópias dos objetos
- **Exceção personalizada:** `ValidacaoException`

## Como funcionam os preços
Cada tipo de serviço pode ter **variações** (ex.: Decoração "Simples - Pequena", "Luxo - Grande"), e cada variação tem um preço para cada dia da semana.
No agendamento, ao escolher serviço, opção e data, o valor é sugerido pela tabela e pode ser alterado. Um agendamento pode ter quantos serviços forem necessários, com quantidade (ex.: 3 garçons).

PDFs gerados com [QuestPDF](https://www.questpdf.com) (licença Community: gratuita para empresas com faturamento anual de até US$ 1 milhão).
