namespace PsychologicalSupportPlatform.Messaging.Domain.DTOs;

public class AddMessageDTO
{
    public AddMessageDTO(int id, string text)
    {
        Text = text;
        ConsumerId = id;
    }
    public AddMessageDTO()
    {
    }

    public string Text { get; set; }
    
    public int ConsumerId { get; set; }
}
