namespace PsychologicalSupportPlatform.Messaging.Domain.DTOs;

public class AddMessageDTO
{
    public AddMessageDTO(int id, string text, int senderId)
    {
        Text = text;
        ConsumerId = id;
        SenderId = senderId;
    }
    public AddMessageDTO()
    {
    }

    public string Text { get; set; }
    
    public int ConsumerId { get; set; }
    
    public int SenderId { get; set; }
}