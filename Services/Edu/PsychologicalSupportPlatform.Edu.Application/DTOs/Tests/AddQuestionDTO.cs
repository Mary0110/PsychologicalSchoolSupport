namespace PsychologicalSupportPlatform.Edu.Application.DTOs.Tests;

public class AddQuestionDTO
{
    public string Text { get; set; } = null!;
    
    public List<AddAnswerDTO> Answers { get; set; } = null!;
}
