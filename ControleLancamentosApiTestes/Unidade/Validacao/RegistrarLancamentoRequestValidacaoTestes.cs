using Bogus;
using ControleLancamentosAPI.Validacoes;
using ControleLancamentosApiTestes.Criadores;
using FluentValidation.TestHelper;

namespace ControleLancamentosApiTestes.Unidade.Validacao;

public class RegistrarLancamentoRequestValidacaoTestes
{
    [Fact]
    public void Deve_ValidarRegistro()
    {
        var validador = new RegistrarLancamentoRequestValidacao();
        var registro = new CriadorRegistrarLancamentoRequest().Generate();

        var resutlado = validador.TestValidate(registro);
        resutlado.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Deve_TerErros_QuandoNaoTiverDescricao()
    {
        var validador = new RegistrarLancamentoRequestValidacao();
        var registro = new CriadorRegistrarLancamentoRequest()
            .ComDescricao(default)
            .Generate();

        var resutlado = validador.TestValidate(registro);
        resutlado.ShouldHaveValidationErrorFor(e => e.Descricao)
            .Only();
    }

    [Fact]
    public void Deve_TerErros_QuandoValorForMenorOuIgualAZero()
    {
        var valorMenorOuIgualAZero = new Faker().Random.Double(double.MinValue, 0);
        var validador = new RegistrarLancamentoRequestValidacao();
        var registro = new CriadorRegistrarLancamentoRequest()
            .ComValor(valorMenorOuIgualAZero)
            .Generate();

        var resutlado = validador.TestValidate(registro);
        resutlado.ShouldHaveValidationErrorFor(e => e.Valor)
            .Only();
    }

    [Fact]
    public void Deve_TerErros_QuandoNaoTiverOperador()
    {
        var validador = new RegistrarLancamentoRequestValidacao();
        var registro = new CriadorRegistrarLancamentoRequest()
            .ComOperador(default)
            .Generate();

        var resutlado = validador.TestValidate(registro);
        resutlado.ShouldHaveValidationErrorFor(e => e.Operador)
            .Only();
    }

    [Fact]
    public void Deve_TerErros_QuandoNaoTiverNatureza()
    {
        var validador = new RegistrarLancamentoRequestValidacao();
        var registro = new CriadorRegistrarLancamentoRequest()
            .ComNatureza(default)
            .Generate();

        var resutlado = validador.TestValidate(registro);
        resutlado.ShouldHaveValidationErrorFor(e => e.Natureza)
            .Only();
    }
}

