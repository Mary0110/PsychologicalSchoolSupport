namespace PsychologicalSupportPlatform.Messaging.Application.Errors;

public class WrongRolesForSendingMessages : InvalidOperationException
{
    public WrongRolesForSendingMessages(string senderRole, string consumerRole)
        : base($"Invalid roles for sending messages: {senderRole} and {consumerRole}")
    {
    }
}
