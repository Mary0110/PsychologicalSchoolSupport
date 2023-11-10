namespace PsychologicalSupportPlatform.Messaging.Application.Services;

public interface IChatClient
{
    Task ReceiveMessage(string message);
}