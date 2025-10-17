using ControleLancamentosAPI.Compartilhado;
using ControleLancamentosAPI.Data.Models;
using MediatR;

namespace ControleLancamentosAPI.Data.Comandos;

public class RegistrarLancamentoRequestHandler : IRequestHandler<RegistrarLancamentoRequest, Resultado<RegistrarLancamentoResponse, Erro>>
{
    private readonly LancamentosContexto _contexto;

    public RegistrarLancamentoRequestHandler(LancamentosContexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<Resultado<RegistrarLancamentoResponse, Erro>> Handle(RegistrarLancamentoRequest request, CancellationToken cancellationToken)
    {
        var lancamento = new LancamentoFinanceiro()
        {
            Descricao = request.Descricao,
            Natureza = request.Natureza,
            Valor = request.Valor,
            Operador = request.Operador,
        };
        _contexto.Lancamento.Add(lancamento);

        var qtdRegistrosSalvos = await _contexto.SaveChangesAsync();
        if (qtdRegistrosSalvos != 1) return new Erro(nameof(RegistrarLancamentoRequestHandler), "Erro ao gravar registro de Lançamento");
        return new RegistrarLancamentoResponse(lancamento.Id.ToString());
    }
}
