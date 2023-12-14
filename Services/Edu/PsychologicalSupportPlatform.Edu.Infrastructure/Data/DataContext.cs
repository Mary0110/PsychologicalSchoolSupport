using Microsoft.EntityFrameworkCore;
using PsychologicalSupportPlatform.Edu.Domain.Entities;
using PsychologicalSupportPlatform.Edu.Domain.Entities.Tests;

namespace PsychologicalSupportPlatform.Edu.Infrastructure.Data;

public class DataContext: DbContext
{
    public DbSet<EduMaterial> EduMaterials { get; set; }
    
    public DbSet<StudentHasEduMaterial> StudentHasEduMaterials { get; set; }
    
    public DbSet<Test> Tests { get; set; }

    public DbSet<Question> Questions { get; set; }

    public DbSet<Answer> Answers { get; set; }

    public DbSet<AnswerRequest> AnswerRequests { get; set; }
    
    public DbSet<TestResult> TestResults { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
}
