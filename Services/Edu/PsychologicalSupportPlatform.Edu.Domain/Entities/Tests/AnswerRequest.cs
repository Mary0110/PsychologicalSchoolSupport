namespace PsychologicalSupportPlatform.Edu.Domain.Entities.Tests;

public class AnswerRequest
{
    public int Id { get; set; }
    
    public int SelectedAnswerId { get; set; }
    
    public Answer SelectedAnswer { get; set; }

    public int StudentHasTestId { get; set; }
    
    public TestResult TestResult{ get; set; }
}
