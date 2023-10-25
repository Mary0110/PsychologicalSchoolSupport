namespace PsychologicalSupportPlatform.Authorization.Domain.Entities;

public class Student: User
{
    public DateTime DateOfBirth { get; set; }
    
    public int FormId { get; set; }
    
    public int Parallel { get; set; }
    
    public char Letter { get; set; }
    
    public Form Form { get; set; } 
    
    public Status Status { get; set; } = Status.Good;
}



public enum Status
{
    Bad,
    Ok,
    Good
}