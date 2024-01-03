namespace PsychologicalSupportPlatform.Edu.Application.DTOs.Tests;

public class TestResultDTO
{
    public int Id { get; set; }
    
    public TestDTO Test { get; set; }

    public int UserId { get; set; }
    
    public DateTime? DatePassed { get; set; } = DateTime.Now;
    
    public List<AnswerRequestDTO> Answers { get; set; }
}
