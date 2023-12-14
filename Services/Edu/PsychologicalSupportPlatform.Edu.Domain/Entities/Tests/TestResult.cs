namespace PsychologicalSupportPlatform.Edu.Domain.Entities.Tests;

public class TestResult
{
    public int Id { get; set; }
    
    public int TestId { get; set; }
    
    public Test Test { get; set; }

    public int UserId { get; set; }
    
    public DateTime? DatePassed { get; set; } = DateTime.Now;
    
    public List<AnswerRequest> Answers { get; set; }
}
