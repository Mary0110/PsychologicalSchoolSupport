namespace PsychologicalSupportPlatform.Report.Application.DTOs;

public class MonthlyReportDTO
{
    public int? PcychologistId { get; set; }
    
    public DateTime? DateTime { get; set; }
    
    public string Filepath { get; set; }
    
    public string? Comments { get; set; }
}
