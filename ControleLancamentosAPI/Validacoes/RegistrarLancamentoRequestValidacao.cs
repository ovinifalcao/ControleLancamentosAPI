using ControleLancamentosAPI.Data.Comandos;
using ControleLancamentosAPI.Data.Models;
using FluentValidation;

namespace ControleLancamentosAPI.Validacoes;

public class RegistrarLancamentoRequestValidacao : AbstractValidator<RegistrarLancamentoRequest>
{
    public RegistrarLancamentoRequestValidacao()
    {
        RuleFor(lancamento => lancamento.Descricao)
            .NotEmpty()
            .NotNull()
            .WithMessage("O registro precisa ter uma descricao.");

        RuleFor(lancamento => lancamento.Natureza)
            .IsInEnum()
            .WithMessage("O rregistro não é de um tipo válido.");

        RuleFor(lancamento => lancamento.Operador)
            .NotEmpty()
            .NotNull()
            .WithMessage("O registro precisa ter um operador.");

        RuleFor(lancamento => lancamento.Valor)
            .Must(valor => valor > 0)
            .WithMessage("O registro precisa ter valor maior do que Zero.");
    }
}

