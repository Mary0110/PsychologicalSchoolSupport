using PsychologicalSupportPlatform.Common.Repository.Specifications;
using PsychologicalSupportPlatform.Report.Domain.Entities;

namespace PsychologicalSupportPlatform.Report.Infrastracture.Data.Specifications;

public class LastMonthReportSpecification: Specification<MeetupReport>
{
    public LastMonthReportSpecification() : base(
        m => m.DateTime!.Value.Month == DateTime.Today.Month)
    {
    }
}
