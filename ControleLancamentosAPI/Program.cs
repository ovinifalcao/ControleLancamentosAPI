using ControleLancamentosAPI.Data;
using ControleLancamentosAPI.Data.Models;
using ControleLancamentosAPI.Endpoints;
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


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapLancamentoEndpoint();

app.UseHttpsRedirection();
app.Run();

public partial class Program();
