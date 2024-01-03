using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Report.Application.Interfaces;
using PsychologicalSupportPlatform.Report.Domain.Entities;
using PsychologicalSupportPlatform.Report.Infrastructure.Data.Specifications;

namespace PsychologicalSupportPlatform.Report.Infrastructure.Data.Repositories;

public class ReportRepository: SQLRepository<DataContext, MeetupReport>, IReportRepository
{
    public ReportRepository(DataContext context) : base(context) { }
    
    public async Task<List<MeetupReport>> GetLastMonthReportsAsync()
    {
        var specification = new LastMonthReportSpecification();
        
        return await GetAllBySpecificationAsync(specification);
    }
}
