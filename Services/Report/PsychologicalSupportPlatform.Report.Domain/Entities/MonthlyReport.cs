namespace PsychologicalSupportPlatform.Report.Domain.Entities;

public class MonthlyReport
{
    public int Id { get; set; }
    
    public DateTime Date { get; set; } = DateTime.Today;
    
    public string? Name { get; set; }
}
