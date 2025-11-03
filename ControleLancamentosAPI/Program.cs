using ControleLancamentosAPI.Data;
using ControleLancamentosAPI.Data.Models;
using ControleLancamentosAPI.Endpoints;
using ControleLancamentosAPI.Servicos;
using ControleLancamentosAPI.Validacoes;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Services.AddDbContext<LancamentosContexto>(options => {
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("lancamentos_postgres_db"),
        options => options.MapEnum<NaturezaEnum>("natureza"));
});
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblyContaining<Program>();
});
builder.Services.AddScoped<RegistrarLancamentoRequestValidacao>();

var configuracoesMensageria = new MensageriaConfiguracoes();
builder.Configuration.GetSection("Mensageria")
    .Bind(configuracoesMensageria);
builder.Services.AddSingleton(configuracoesMensageria);
builder.Services.AddScoped<IMensageria, Mensageria>();


var app = builder.Build();

using (var scopo = app.Services.CreateScope())
{
    var contexto = scopo.ServiceProvider.GetRequiredService<LancamentosContexto>();
    contexto.Database.EnsureDeleted();
    contexto.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapLancamentoEndpoint();

app.UseHttpsRedirection();
app.Run();

public partial class Program();
