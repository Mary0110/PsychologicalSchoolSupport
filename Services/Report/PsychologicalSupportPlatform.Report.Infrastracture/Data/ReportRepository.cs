using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Report.Application.Interfaces;
using PsychologicalSupportPlatform.Report.Domain.Entities;

namespace PsychologicalSupportPlatform.Report.Infrastracture.Data;

public class ReportRepository: SQLRepository<DataContext, MeetupReport>, IReportRepository
{
    public ReportRepository(DataContext context) : base(context) { }
}
