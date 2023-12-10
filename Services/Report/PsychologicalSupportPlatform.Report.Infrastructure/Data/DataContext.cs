using Microsoft.EntityFrameworkCore;
using PsychologicalSupportPlatform.Report.Domain.Entities;

namespace PsychologicalSupportPlatform.Report.Infrastructure.Data;

public class DataContext: DbContext
{
    public DbSet<MeetupReport> MeetupReports { get; set; }

    public DbSet<MonthlyReport> MonthlyReports { get; set; }
    
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
}
