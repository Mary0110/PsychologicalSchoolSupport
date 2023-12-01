using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Report.Application.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PsychologicalSupportPlatform.Report.Application.Services;

 public class RabbitMQBackgroundConsumerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQBackgroundConsumerService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        var factory = new ConnectionFactory(){ HostName = "rabbitmq" }; //TODO:to appsettings
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceProvider.CreateScope())
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
                    var body = e.Body.ToArray();
                    var messageJson = Encoding.UTF8.GetString(body);
                    var message = JsonConvert.DeserializeObject<MeetupMessageObject>(messageJson);
                    
                    var transientService = scope.ServiceProvider.GetRequiredService<ReportService>();
                    var result = await transientService.AddMeetupReportAsync(message);
                };

                _channel.BasicConsume(queue: "meetup-info",
                    autoAck: true,
                    consumer: consumer);
            });
        }
    }
}