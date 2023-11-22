using PsychologicalSupportPlatform.Authorization.Domain.Entities;

namespace PsychologicalSupportPlatform.Authorization.Domain.DTOs;

public class AddFormDTO
{
    public AddFormDTO(int num, char letter)
    {
        Letter = letter;
        Parallel = num;
    }
    public AddFormDTO()
    {
    }
    
    public char Letter { get; set; } = Constants.A;
    
    public int Parallel { get; set; }
}
