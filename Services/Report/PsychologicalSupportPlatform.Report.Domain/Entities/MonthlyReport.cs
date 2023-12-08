namespace PsychologicalSupportPlatform.Report.Domain.Entities;

public class MonthlyReport
{
    public int Id { get; set; }
    
    public DateTime DateTime { get; set; } = DateTime.Today;
    
    public string? Filepath { get; set; }
}
