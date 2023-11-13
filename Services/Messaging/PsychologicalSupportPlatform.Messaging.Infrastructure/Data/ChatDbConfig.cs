namespace PsychologicalSupportPlatform.Messaging.Infrastructure.Data;

public class ChatDbConfig
{
    public string ConnectionURI { get; set; } = null!;
    
    public string DatabaseName { get; set; } = null!;
    
    public string CollectionName { get; set; } = null!;
}
