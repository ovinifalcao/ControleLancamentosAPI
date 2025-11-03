using Microsoft.OpenApi.Models;

namespace ControleLancamentosAPI.Configuracoes;

public static class SwaggerConfiguracoes
{
    public static IServiceCollection ConfigurarSwagger(this IServiceCollection services, IConfiguration configuracoes)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Controle de Lançamentos API", Version = "v1" });

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Insira seu token Bearer (sem a palavra Bearer)",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
            };

            c.AddSecurityDefinition("Bearer", securityScheme);
            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            };

            c.AddSecurityRequirement(securityRequirement);
        });
        return services;
    }
}
