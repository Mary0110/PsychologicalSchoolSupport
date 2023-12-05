using Microsoft.EntityFrameworkCore;
using PsychologicalSupportPlatform.Meet.Domain.Entities;
using PsychologicalSupportPlatform.Meet.Infrastructure.Data.Comparers;
using PsychologicalSupportPlatform.Meet.Infrastructure.Data.Configurators;
using PsychologicalSupportPlatform.Meet.Infrastructure.Data.Converters;

namespace PsychologicalSupportPlatform.Meet.Infrastructure.Data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<ScheduleCell> ScheduleCells { get; set; }

    public DbSet<Meetup> Meetups { get; set; }

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
