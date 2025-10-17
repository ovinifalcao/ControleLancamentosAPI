using ControleLancamentosAPI.Data.Models;
using FluentAssertions;

namespace ControleLancamentosApiTestes.Unidade.Models;

public class LancamentoFinanceiroTestes
{
    [Fact]
    public void Deve_GerarGuidValido()
    {
        var lancamento = new LancamentoFinanceiro()
        {
            Descricao = default,
            Natureza = default,
            Operador = default,
            Valor = default,
        };

        Guid guidLancamento;
        var guidValido = Guid.TryParse(lancamento.Id.ToString(), out guidLancamento);
        guidValido.Should().BeTrue();
    }

    [Fact]
    public void Deve_GerarDataValida()
    {
        var lancamento = new LancamentoFinanceiro() 
        { 
            Descricao = default,
            Natureza = default,
            Operador = default,
            Valor = default,
        };

        lancamento.Data.Should().BeBefore(DateTime.Now);
    }
}

