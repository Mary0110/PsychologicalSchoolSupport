namespace PsychologicalSupportPlatform.Edu.Domain.Entities.Tests;

public class Question
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;
    
    public int TestId { get; set; }
    
    public Test Test { get; set; } = null!;

    public List<Answer> Answers { get; set; } = null!;
    
    public List<AnswerRequest> AnswersRequests { get; set; }
}
