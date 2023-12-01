using System.Text;
using PsychologicalSupportPlatform.Meet.Application.Interfaces;
using RabbitMQ.Client;

namespace PsychologicalSupportPlatform.Meet.Infrastructure.Services;

public class RabbitMQMessagingService : IRabbitMQMessagingService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQMessagingService()
    {
        var factory = new ConnectionFactory() { HostName = "rabbitmq" }; //TODO: appsettings
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
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
