namespace PsychologicalSupportPlatform.Report.Domain.Entities;

public class QuarterReport
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    public string SenderId { get; set; }
    
    public string ConsumerId { get; set; }
    
    public string Text { get; set; } = string.Empty;
    
    public DateTime DateTime { get; set; }
}