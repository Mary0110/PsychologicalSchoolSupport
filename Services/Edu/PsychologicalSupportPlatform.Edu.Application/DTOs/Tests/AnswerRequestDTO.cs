namespace PsychologicalSupportPlatform.Edu.Application.DTOs.Tests;

public class AnswerRequestDTO
{
    public int TestId { get; set; }
    
    public List<QuestionResultDTO> QuestionResultDTOs { get; set; }
}
