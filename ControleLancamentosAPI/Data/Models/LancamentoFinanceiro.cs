namespace ControleLancamentosAPI.Data.Models;

public class LancamentoFinanceiro
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public DateTime Data { get; private set; } = DateTime.Now;

    public required NaturezaEnum Natureza { get; set; }

    public required string Operador { get; set; }

    public required string Descricao { get; set; }

    public required double Valor { get; set; }

    public long NumeroLancamento { get; set; }
}
