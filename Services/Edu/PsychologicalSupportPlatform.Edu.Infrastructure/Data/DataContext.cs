using Microsoft.EntityFrameworkCore;
using PsychologicalSupportPlatform.Edu.Domain.Entities;

namespace PsychologicalSupportPlatform.Edu.Infrastructure.Data;

public class DataContext: DbContext
{
    public DbSet<EduMaterial> EduMaterials { get; set; }
    
    public DbSet<StudentHasEduMaterial> StudentHasEduMaterials { get; set; }
    
    public DbSet<Test> Tests { get; set; }

    public DbSet<Question> Questions { get; set; }

    public DbSet<Answer> Answers { get; set; }

    public DbSet<QuestionResult> QuestionResults { get; set; }
    
    public DbSet<UserTestResult> UserTestResults { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
}
