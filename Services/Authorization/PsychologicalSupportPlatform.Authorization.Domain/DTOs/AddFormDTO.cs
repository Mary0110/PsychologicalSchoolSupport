using PsychologicalSupportPlatform.Authorization.Domain.Entities;

namespace PsychologicalSupportPlatform.Authorization.Domain.DTOs;

public class AddFormDTO
{
    public char Letter { get; set; } = 'A';
    
    public int Parallel { get; set; }
}