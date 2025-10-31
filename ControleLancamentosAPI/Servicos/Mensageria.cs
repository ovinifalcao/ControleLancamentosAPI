
using Confluent.Kafka;

namespace ControleLancamentosAPI.Servicos;

public class Mensageria : IMensageria
{
    private readonly MensageriaConfiguracoes _configuracoes;

    public Mensageria(MensageriaConfiguracoes configuracoes)
    {
        _configuracoes = configuracoes;
    }

    public async Task PublicarEvento(string conteudo)
    {
        var configuracoesProdutor = new ProducerConfig
        {
            BootstrapServers = _configuracoes.Servidor
        };

        using var produtor = new ProducerBuilder<string, string>(configuracoesProdutor)
        .SetValueSerializer(Serializers.Utf8)
        .Build();

        var deliveryReport = await produtor.ProduceAsync(_configuracoes.Topico,
            new Message<string, string> { Value = conteudo });
    }
}
