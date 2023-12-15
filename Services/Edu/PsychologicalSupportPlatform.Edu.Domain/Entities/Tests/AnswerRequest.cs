namespace PsychologicalSupportPlatform.Edu.Domain.Entities.Tests;

public class AnswerRequest
{
    public int Id { get; set; }
    
    public int SelectedAnswerId { get; set; }
    
    public Answer SelectedAnswer { get; set; }

    public int QuestionId { get; set; }
    
    public Question Question { get; set; }
    
    public int TestResultId { get; set; }
    
    public TestResult TestResult{ get; set; }
}
