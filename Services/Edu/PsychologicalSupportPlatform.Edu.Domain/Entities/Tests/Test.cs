namespace PsychologicalSupportPlatform.Edu.Domain.Entities.Tests;

public class Test
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public List<Question> Questions { get; set; }
    
    public List<TestResult> StudentHasTest { get; set; }
}
