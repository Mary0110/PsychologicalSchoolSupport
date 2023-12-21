namespace PsychologicalSupportPlatform.Edu.Domain.Entities;

public class EduMaterial
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Theme { get; set; } = string.Empty;
    
    public List<StudentHasEduMaterial>? StudentHasEduMaterials { get; set; }
}