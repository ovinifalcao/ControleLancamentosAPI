using ControleLancamentosAPI.Data;
using ControleLancamentosAPI.Data.Models;
using ControleLancamentosAPI.Servicos;
using ControleLancamentosAPI.Validacoes;
using Microsoft.EntityFrameworkCore;

namespace ControleLancamentosAPI.Configuracoes;

public static class ServicosConfiguracoes
{
    public static IServiceCollection ConfigurarServicos(this IServiceCollection service, IConfiguration configuracoes)
    {
        service.AddEndpointsApiExplorer();
        service.AddSwaggerGen();
        service.AddLogging();
        service.AddDbContext<LancamentosContexto>(options => {
            options.UseNpgsql(
                configuracoes.GetConnectionString("lancamentos_postgres_db"),
                options => options.MapEnum<NaturezaEnum>("natureza"));
        });
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        service.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<Program>();
        });
        service.AddScoped<RegistrarLancamentoRequestValidacao>();

        var configuracoesMensageria = new MensageriaConfiguracoes();
        configuracoes.GetSection("Mensageria")
            .Bind(configuracoesMensageria);
        service.AddSingleton(configuracoesMensageria);
        service.AddScoped<IMensageria, Mensageria>();
        return service;
    }
}
