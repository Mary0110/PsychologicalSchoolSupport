using Microsoft.EntityFrameworkCore;
using PsychologicalSupportPlatform.Report.Domain.Entities;
using PsychologicalSupportPlatform.Report.Infrastracture.Data.Configurators;

namespace PsychologicalSupportPlatform.Report.Infrastracture.Data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<MeetupReport> MeetupReports { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MeetupReportConfigurator());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        base.ConfigureConventions(builder); 
    }
}
