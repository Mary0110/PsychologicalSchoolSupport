namespace PsychologicalSupportPlatform.Edu.Domain.Entities;

public class QuestionResult
{
    public int Id { get; set; }
    
    public int SelectedAnswerId { get; set; }
    
    public Answer SelectedAnswer { get; set; }
    
    public int UserTestResultId { get; set; }
    
    public UserTestResult UserTestResult{ get; set; }
}
