using ControleLancamentosAPI.Compartilhado;
using ControleLancamentosAPI.Data.Models;
using ControleLancamentosAPI.Servicos;
using MediatR;
using System.Text.Json;

namespace ControleLancamentosAPI.Data.Comandos;

public class RegistrarLancamentoRequestHandler : IRequestHandler<RegistrarLancamentoRequest, Resultado<RegistrarLancamentoResponse, Erro>>
{
    private readonly LancamentosContexto _contexto;
    private readonly IMensageria _mensageria;

    public RegistrarLancamentoRequestHandler(LancamentosContexto contexto, IMensageria mensageria)
    {
        _contexto = contexto;
        _mensageria = mensageria;
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

        var lacamentoMensagem = JsonSerializer.Serialize(lancamento);
        await  _mensageria.PublicarEvento(lacamentoMensagem);

        var qtdRegistrosSalvos = await GravarLancamentoEmBanco(lancamento);
        if (qtdRegistrosSalvos != 1) return new Erro(nameof(RegistrarLancamentoRequestHandler), "Erro ao gravar registro de Lançamento");
        return new RegistrarLancamentoResponse(lancamento.Id.ToString());
    }

    private async Task<int> GravarLancamentoEmBanco(LancamentoFinanceiro lancamento)
    {
        _contexto.Lancamento.Add(lancamento);
        return await _contexto.SaveChangesAsync();
    }
}
