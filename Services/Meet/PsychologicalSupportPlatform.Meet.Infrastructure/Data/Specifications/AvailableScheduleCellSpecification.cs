using PsychologicalSupportPlatform.Common.Repository.Specifications;
using PsychologicalSupportPlatform.Meet.Domain.Entities;

namespace PsychologicalSupportPlatform.Meet.Infrastructure.Data.Specifications;

public class AvailableScheduleCellSpecification: Specification<ScheduleCell>
{
    public AvailableScheduleCellSpecification() : base(p =>
        (p.Active && p.Time >= TimeOnly.FromDateTime(DateTime.Now)) &&
        (p.Meetups.Any(c => c.Date >= DateOnly.FromDateTime(DateTime.Now.Date))))
    {
    }
}
