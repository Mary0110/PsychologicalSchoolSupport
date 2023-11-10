using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PsychologicalSupportPlatform.Messaging.Domain.Entities;

public class Message
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public int Id { get; set; }
    
    public int SenderId { get; set; }
    
    public int ConsumerId { get; set; }

    public string Text { get; set; } = string.Empty;
    
    public DateTime DateTime { get; set; }
}