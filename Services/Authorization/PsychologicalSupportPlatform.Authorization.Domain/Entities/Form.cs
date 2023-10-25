using System.ComponentModel;

namespace PsychologicalSupportPlatform.Authorization.Domain.Entities;

public class Form
{ 
    public char Letter { get; set; } = 'A';
    
    public int Parallel { get; set; }
    
    public List<Student> Students { get; set; }
}
