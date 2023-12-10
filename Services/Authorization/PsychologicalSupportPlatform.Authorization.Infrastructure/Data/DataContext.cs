using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PsychologicalSupportPlatform.Authorization.Domain.Entities;

namespace PsychologicalSupportPlatform.Authorization.Infrastructure.Data;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Student> Students { get; set; }
    
    public DbSet<Form> Forms { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfigurator());
        modelBuilder.ApplyConfiguration(new StudentConfigurator());
        modelBuilder.ApplyConfiguration(new FormConfigurator());
    }
}
