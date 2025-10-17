using Bogus;
using ControleLancamentosAPI.Data;
using ControleLancamentosAPI.Data.Comandos;
using ControleLancamentosApiTestes.Configuracoes.Base;
using ControleLancamentosApiTestes.Criadores;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;

namespace ControleLancamentosApiTestes.Integracao.Endpoints;

[Collection("ApiCollection")]
public class RegistrarLancamentoRequestHandlerTestes
{
    private readonly LancamentosContexto _contexto;
    private readonly HttpClient _client;

    public RegistrarLancamentoRequestHandlerTestes(ApiTesteBase fixture)
    {
        _contexto = fixture.GetScopedService<LancamentosContexto>();
        _client = fixture.GetHttpClient();
    }

    [Fact]
    public async Task Deve_GravarRegistroCorreamente()
    {
        var registro = new CriadorRegistrarLancamentoRequest().Generate();
        var requsicao = JsonSerializer.Serialize(registro);
        var conteudoRequisicao = new StringContent(requsicao, Encoding.UTF8, "application/json");

        var resposta = await _client.PostAsync("/Lancamento", conteudoRequisicao);
        var conteudoResposta = await resposta.Content.ReadAsStringAsync();
        var valorResposta = JsonSerializer.Deserialize<string>(conteudoResposta);

        var registroGravado = await _contexto.Lancamento
            .Where(l => l.Id == Guid.Parse(valorResposta))
            .FirstAsync();

        registro.Should().BeEquivalentTo(registroGravado, opt =>
                    opt.Excluding(x => x.Id)
                    .Excluding(x => x.Data)
                    .Excluding(x => x.NumeroLancamento));
    }

    [Fact]
    public async Task Deve_ResponderBadRequest_QuandoTiverProblemasDeValidacao()
    {
        var registro = new RegistrarLancamentoRequest(default, default, default, default);
        var requsicao = JsonSerializer.Serialize(registro);
        var conteudoRequisicao = new StringContent(requsicao, Encoding.UTF8, "application/json");

        var resposta = await _client.PostAsync("/Lancamento", conteudoRequisicao);
        resposta.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }
}
