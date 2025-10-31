namespace ControleLancamentosAPI.Servicos;

public interface IMensageria
{
    public Task PublicarEvento(string conteudo);
}
