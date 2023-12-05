using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Config;
using PsychologicalSupportPlatform.Report.Application.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PsychologicalSupportPlatform.Report.Application.Services;

 public class RabbitMQBackgroundConsumerService : BackgroundService
{
    public IServiceProvider Services { get; }
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQBackgroundConsumerService(IServiceProvider services, IOptions<RabbitMQConfig> rabbitMQConfig)
    {
        Services = services;
        var factory = new ConnectionFactory(){ HostName = rabbitMQConfig.Value.HostName }; 
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        await Task.Run(() =>
        {
            _channel.QueueDeclare(queue: "meetup-info",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, e) =>
            {
                using (var scope = Services.CreateScope())
                {
                    var body = e.Body.ToArray();
                    var messageJson = Encoding.UTF8.GetString(body);
                    var message = JsonConvert.DeserializeObject<MeetupMessageObject>(messageJson);
                    
                    var transientService = scope.ServiceProvider.GetRequiredService<IReportService>();
                    var result = await transientService.AddMeetupReportAsync(message);
                }
            };

            _channel.BasicConsume(queue: "meetup-info",
                autoAck: true,
                consumer: consumer);
        });
    }
}
