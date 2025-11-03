using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ControleLancamentosAPI.Configuracoes;

public static class AutenticacaoConfiguracoes
{
    public static IServiceCollection ConfigurarAutenticacao(this IServiceCollection servicos, IConfiguration configuracao)
    {
        servicos.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata = false;
            o.Audience = configuracao["Authentication:Audience"];
            o.MetadataAddress = configuracao["Authentication:MetadataAddress"]!;
            o.TokenValidationParameters.RoleClaimType = "roles";
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = configuracao["Authentication:ValidIssuer"]
            };
        });

        return servicos;
    }

    public static IServiceCollection ConfigurarAutorizacao(this IServiceCollection servicos, IConfiguration configuracao)
    {
        servicos.AddAuthorization(options =>
        {
            options.AddPolicy("OperadorRole", policy => policy.RequireRole("lancamentos-api-acess"));
        });

        return servicos;
    }
}
