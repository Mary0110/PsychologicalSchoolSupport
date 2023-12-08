namespace PsychologicalSupportPlatform.Report.Application.DTOs;

public class ReportDTO
{
    public int? CreatorId { get; set; }
    
    public DateTime? DateTime { get; set; }
    
    public int StudentId { get; set; }
    
    public int MeetupId { get; set; }
    
    public string? Comments { get; set; }
    
    public string? Conclusion { get; set; }
}
