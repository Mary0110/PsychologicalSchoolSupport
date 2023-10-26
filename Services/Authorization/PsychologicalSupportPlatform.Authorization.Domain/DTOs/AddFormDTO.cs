using PsychologicalSupportPlatform.Authorization.Domain.Entities;

namespace PsychologicalSupportPlatform.Authorization.Domain.DTOs;

public class AddFormDTO
{
    public char Letter { get; set; } = Constants.A;
    
    public int Parallel { get; set; }
}
