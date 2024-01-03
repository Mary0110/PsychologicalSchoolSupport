namespace PsychologicalSupportPlatform.Authorization.Domain.DTOs;

public class AddStudentDTO
{
    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public string Patronymic { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    
    public DateTime DateOfBirth { get; set; }

    public int Parallel { get; set; } 
    
    public char Letter { get; set; } = Constants.A;
}
