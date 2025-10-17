using ControleLancamentosAPI.Compartilhado;
using ControleLancamentosAPI.Data.Models;
using MediatR;

namespace ControleLancamentosAPI.Data.Comandos;

public record RegistrarLancamentoRequest(
    NaturezaEnum Natureza,
    string Operador,
    string Descricao,
    double Valor)
    : IRequest<Resultado<RegistrarLancamentoResponse, Erro>>;