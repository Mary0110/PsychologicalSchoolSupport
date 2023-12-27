using System.Text;
using Microsoft.Extensions.Options;
using PsychologicalSupportPlatform.Common.Config;
using PsychologicalSupportPlatform.Meet.Application.Interfaces;
using RabbitMQ.Client;

namespace PsychologicalSupportPlatform.Meet.Infrastructure.Services;

public class RabbitMQMessagingService : IRabbitMQMessagingService
{
    private readonly IModel _channel;

    public RabbitMQMessagingService(IOptions<RabbitMQConfig> rabbitMQConfig)
    {
        var factory = new ConnectionFactory() { HostName = rabbitMQConfig.Value.HostName };
        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();
    }

    public void PublishMessage(string queue, string message)
    {
        _channel.QueueDeclare(queue: queue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: "",
            routingKey: queue,
            basicProperties: null,
            body: body);
    }
}
