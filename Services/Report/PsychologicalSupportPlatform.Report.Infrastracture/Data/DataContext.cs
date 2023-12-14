using Microsoft.EntityFrameworkCore;
using PsychologicalSupportPlatform.Report.Domain.Entities;

namespace PsychologicalSupportPlatform.Report.Infrastracture.Data;

public class DataContext: DbContext
{
    public DbSet<MeetupReport> MeetupReports { get; set; }
    
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
}
