namespace PsychologicalSupportPlatform.Report.Domain.Entities;

public class MeetupReport
{
    public int Id { get; set; }
    
    public int? CreatorId { get; set; }
    
    public DateTime? DateTime { get; set; }
    
    public int StudentId { get; set; }
    
    public int MeetupId { get; set; }
    
    public string? Comments { get; set; }
    
    public string? Conclusion { get; set; }
}
