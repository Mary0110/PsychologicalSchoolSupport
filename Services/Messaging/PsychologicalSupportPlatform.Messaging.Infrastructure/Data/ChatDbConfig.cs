namespace PsychologicalSupportPlatform.Messaging.Infrastructure.Data;

public class ChatDbConfig
{
    public string ConnectionString { get; set; } = null!;
    
    public string DatabaseName { get; set; } = null!;
    
    public string ChatsCollectionName { get; set; } = null!;
}