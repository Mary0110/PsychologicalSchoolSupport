namespace PsychologicalSupportPlatform.Authorization.Domain.Entities;

public class User
{
    public int Id { get; set; }
      
    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public string? Patronymic { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Role { get; set; }
}
