namespace PsychologicalSupportPlatform.Edu.Domain.Entities;

public class StudentHasEduMaterial
{
    public int Id { get; set; }

    public int StudentId { get; set; }
    
    public int EduMaterialId { get; set; }
    
    public DateTime Date { get; set; } = DateTime.Now;
    
    public EduMaterial EduMaterial { get; set; } = null!;
}
