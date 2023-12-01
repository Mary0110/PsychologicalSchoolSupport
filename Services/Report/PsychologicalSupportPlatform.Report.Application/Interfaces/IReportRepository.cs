using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Report.Domain.Entities;

namespace PsychologicalSupportPlatform.Report.Application.Interfaces;

public interface IReportRepository : ISQLRepository<MeetupReport>
{
    
}
