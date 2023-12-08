using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Report.Application.Interfaces;
using PsychologicalSupportPlatform.Report.Domain.Entities;
using PsychologicalSupportPlatform.Report.Infrastracture.Data.Specifications;

namespace PsychologicalSupportPlatform.Report.Infrastracture.Data.Repositories;

public class MonthlyReportRepository: SQLRepository<DataContext, MonthlyReport>, IMonthlyReportRepository
{
    public MonthlyReportRepository(DataContext context) : base(context) { }
}
