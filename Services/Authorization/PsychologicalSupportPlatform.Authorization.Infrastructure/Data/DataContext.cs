using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PsychologicalSupportPlatform.Authorization.Domain.Entities;

namespace PsychologicalSupportPlatform.Authorization.Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    public DbSet<Student> Students { get; set; }
    
    public DbSet<Form> Forms { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Student>().ToTable("Students");
        modelBuilder.Entity<Form>().HasKey(u => new { u.Parallel, u.Letter });
        modelBuilder.Entity<Form>().HasMany(e => e.Students)
            .WithOne(e => e.Form)
            .HasForeignKey(e => new {e.Parallel, e.Letter})
            .IsRequired();
    }
}
