namespace PsychologicalSupportPlatform.Edu.Application.DTOs.Tests;

public class PassTestDTO
{
    public string UserId { get; set; }

    public int TestId { get; set; }
    
    public List<AnswerRequestDTO> Answers { get; set; }
}
