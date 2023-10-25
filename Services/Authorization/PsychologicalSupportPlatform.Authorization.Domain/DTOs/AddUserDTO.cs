using PsychologicalSupportPlatform.Authorization.Domain.Entities;

namespace PsychologicalSupportPlatform.Authorization.Domain.DTOs;

public class AddUserDTO
{
    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public string Patronymic { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public Role Role { get; set; }
    
}