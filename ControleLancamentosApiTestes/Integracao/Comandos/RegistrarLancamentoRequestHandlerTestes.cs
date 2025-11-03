using Bogus;
using ControleLancamentosAPI.Data;
using ControleLancamentosAPI.Data.Comandos;
using ControleLancamentosAPI.Servicos;
using ControleLancamentosApiTestes.Configuracoes.Base;
using ControleLancamentosApiTestes.Criadores;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace ControleLancamentosApiTestes.Integracao.Comandos
{
    [Collection("DatabaseCollection")]
    public class RegistrarLancamentoRequestHandlerTestes
    {
        private readonly Faker _faker;
        private readonly LancamentosContexto _contexto;
        private readonly IMensageria _mensageria;

        public RegistrarLancamentoRequestHandlerTestes(DatabaseTesteBase fixture)
        {
            _faker = fixture.Faker;
            _contexto = fixture._contexto;
            _mensageria = Substitute.For<IMensageria>();
        }

        [Fact]
        public async Task Deve_GravarRegistroDeLancamentoCorretamente()
        {
            var registro = new CriadorRegistrarLancamentoRequest().Generate();
            var handler = new RegistrarLancamentoRequestHandler(_contexto, _mensageria);

            var resultado = await handler.Handle(registro, new CancellationToken());

            var registroGravado = await _contexto.Lancamento
                .Where(l => l.Id == Guid.Parse(resultado!.Valor.Id!))
                .FirstAsync();

            using (new AssertionScope())
            {
                registroGravado.Should().NotBeNull();
                registro.Should().BeEquivalentTo(registroGravado, opt => 
                    opt.Excluding(x => x.Id)
                    .Excluding(x => x.Data)
                    .Excluding(x => x.NumeroLancamento));

            }
        }

        [Fact]
        public async Task Deve_GravarRegistroDeLancamentoComNumeroDeLancamento()
        {
            var registro = new CriadorRegistrarLancamentoRequest().Generate();
            var handler = new RegistrarLancamentoRequestHandler(_contexto, _mensageria);

            var resultado = await handler.Handle(registro, new CancellationToken());

            var registroGravado = await _contexto.Lancamento
                .Where(l => l.Id == Guid.Parse(resultado!.Valor.Id!))
                .FirstAsync();

            using (new AssertionScope())
            {
                registroGravado.Should().NotBeNull();
                registroGravado.NumeroLancamento.Should().BeGreaterThanOrEqualTo(10000000);
            }
        }
    }
}
