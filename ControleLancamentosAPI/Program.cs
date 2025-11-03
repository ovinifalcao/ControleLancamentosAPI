using ControleLancamentosAPI.Configuracoes;
using ControleLancamentosAPI.Data;
using ControleLancamentosAPI.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .ConfigurarServicos(builder.Configuration)
    .ConfigurarAutorizacao(builder.Configuration)
    .ConfigurarAutenticacao(builder.Configuration)
    .ConfigurarSwagger(builder.Configuration)
    .AddAuthorizationBuilder();

var app = builder.Build();

using (var scopo = app.Services.CreateScope())
{
    var contexto = scopo.ServiceProvider.GetRequiredService<LancamentosContexto>();
    contexto.Database.EnsureDeleted();
    contexto.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();
app.MapLancamentoEndpoint(!app.Environment.IsDevelopment());
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.Run();

public partial class Program();
