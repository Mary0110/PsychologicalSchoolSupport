using Microsoft.EntityFrameworkCore;
using PsychologicalSupportPlatform.Meet.Domain.Entities;
using PsychologicalSupportPlatform.Meet.Infrastructure.Data.Configurators;

namespace PsychologicalSupportPlatform.Meet.Infrastructure.Data;

public class DataContext: DbContext
{
    public DbSet<ScheduleCell> ScheduleCells { get; set; }

    public DbSet<Meetup> Meetups { get; set; }
    
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ScheduleCellConfigurator());
        modelBuilder.ApplyConfiguration(new MeetupConfigurator());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        base.ConfigureConventions(builder);
        ModelConfigurator.ApplyConvention(builder);
    }
}
