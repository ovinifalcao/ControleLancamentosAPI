using ControleLancamentosAPI.Compartilhado.Erros;
using ControleLancamentosAPI.Data.Comandos;
using ControleLancamentosAPI.Validacoes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ControleLancamentosAPI.Endpoints;

public static class LancamentosEndpoint
{
    public static void MapLancamentoEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/Lancamento", RegistrarLancamento);
    }

    public static async Task<IResult> RegistrarLancamento(
        [FromBody] RegistrarLancamentoRequest lancamento,
        IMediator mediator,
        RegistrarLancamentoRequestValidacao validador,
        [FromServices] ILogger<IEndpointsEntryPoint> logger)
    {
        var resultadoValidacao = await validador.ValidateAsync(lancamento);
        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors.ToArray().ToString();
            logger.RegistroInvalido(erros!);
            return Results.BadRequest(erros!);
        }

        var resultadoOperacao = await mediator.Send(lancamento);
        return resultadoOperacao.Corresponder<IResult>(
            sucesso => Results.Ok(sucesso.Id),
            falha => Results.Problem(falha.Mensagem));
    }

}
