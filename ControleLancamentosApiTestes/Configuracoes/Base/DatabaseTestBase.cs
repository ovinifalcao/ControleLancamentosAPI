using Bogus;
using ControleLancamentosAPI.Data;
using ControleLancamentosAPI.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ControleLancamentosApiTestes.Configuracoes.Base;

public class DatabaseTestBase
{
    public readonly LancamentosContexto _contexto;
    private readonly IConfiguration _configuration;
    protected IServiceProvider _serviceScope;
    public Faker Faker = new Faker("pt_BR");

    public DatabaseTestBase()
    {
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        _serviceScope = new ServiceCollection()
            .AddDbContext<LancamentosContexto>(options => {
                options.UseNpgsql(
                    _configuration.GetConnectionString("lancamentos_postgres_db"),
                    options => options.MapEnum<NaturezaEnum>("natureza"));
            })
            .BuildServiceProvider(false);

        _contexto = GetScopedService<LancamentosContexto>();
        _contexto.Database.EnsureDeleted();
        _contexto.Database.EnsureCreated();
    }

    public T GetScopedService<T>() where T : class => _serviceScope.CreateScope().ServiceProvider.GetService<T>();
}