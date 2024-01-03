using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Report.Application.Interfaces;
using PsychologicalSupportPlatform.Report.Domain.Entities;

namespace PsychologicalSupportPlatform.Report.Infrastructure.Data.Repositories;

public class MonthlyReportRepository: SQLRepository<DataContext, MonthlyReport>, IMonthlyReportRepository
{
    public MonthlyReportRepository(DataContext context) : base(context) { }
}
