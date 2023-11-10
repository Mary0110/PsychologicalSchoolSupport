namespace PsychologicalSupportPlatform.Messaging.Domain.DTOs;

public class MessageDTO
{
    public MessageDTO(int senderId, int consumerId, string text, DateTime dateTime)
    {
        SenderId = senderId;
        ConsumerId = consumerId;
        Text = text;
        DateTime = dateTime;
    }
    
    public MessageDTO()
    {
    }
    
    public int SenderId { get; set; }
    
    public int ConsumerId { get; set; }

    public string Text { get; set; } = string.Empty;
    
    public DateTime DateTime { get; set; }
}
