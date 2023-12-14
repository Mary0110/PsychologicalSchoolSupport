namespace PsychologicalSupportPlatform.Edu.Application.DTOs.Tests;

public class QuestionDTO
{
    public string Text { get; set; } = null!;
    
    public List<AnswerDTO> Answers { get; set; } = null!;
}
