namespace PsychologicalSupportPlatform.Edu.Domain.Entities;

public class UserTestResult
{
    public int Id { get; set; }

    public int UserId { get; set; }
    
    public DateTime? DatePassed { get; set; } = DateTime.Now;

    public List<QuestionResult> QuestionResults { get; set; }
}
