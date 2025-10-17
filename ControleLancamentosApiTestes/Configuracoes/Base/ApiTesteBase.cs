using Microsoft.Extensions.DependencyInjection;

namespace ControleLancamentosApiTestes.Configuracoes.Base;

public class ApiTesteBase
{
    private readonly LancamentosWebFactory _factory;
    protected readonly HttpClient _client;

    public ApiTesteBase()
    {
        _factory = new LancamentosWebFactory();
        _client = _factory.CreateClient();
    }

    public T GetScopedService<T>() where T : class => _factory.Services.CreateScope().ServiceProvider.GetService<T>();

    public HttpClient GetHttpClient() => _client;
}