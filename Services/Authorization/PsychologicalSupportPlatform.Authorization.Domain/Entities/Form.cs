namespace PsychologicalSupportPlatform.Authorization.Domain.Entities;

public class Form
{ 
    public char Letter { get; set; } = Constants.A;
    
    public int Parallel { get; set; }
    
    public List<Student> Students { get; set; }
}
