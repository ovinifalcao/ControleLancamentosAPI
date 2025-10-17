using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace ControleLancamentosApiTestes.Configuracoes.Base;

public class LancamentosWebFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var configs = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        builder.UseEnvironment("Test");

        builder.ConfigureServices(servicos => {
            using (var scope = servicos.BuildServiceProvider().CreateScope())
            {
            }
        });
        base.ConfigureWebHost(builder);
    }
}
