namespace PsychologicalSupportPlatform.Meet.Application.Interfaces;

public interface IRabbitMQMessagingService
{
    void PublishMessage(string queue, string message);
}
