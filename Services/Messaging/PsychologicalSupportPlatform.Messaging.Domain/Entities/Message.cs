using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PsychologicalSupportPlatform.Messaging.Domain.Entities;

public class Message
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    public string SenderId { get; set; }
    
    public string ConsumerId { get; set; }
    
    public string Text { get; set; } = string.Empty;
    
    public DateTime DateTime { get; set; }
    
    public Message(string consumerId, string senderId, string text, DateTime dateTime)
    {
        ConsumerId = consumerId;
        SenderId = senderId;
        Text = text;
        DateTime = dateTime;
    }
}
