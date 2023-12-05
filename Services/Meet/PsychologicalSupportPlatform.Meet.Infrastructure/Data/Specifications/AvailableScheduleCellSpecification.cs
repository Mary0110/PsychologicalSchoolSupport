using PsychologicalSupportPlatform.Common.Repository.Specifications;
using PsychologicalSupportPlatform.Meet.Domain.Entities;

namespace PsychologicalSupportPlatform.Meet.Infrastructure.Data.Specifications;

public class AvailableScheduleCellSpecification: Specification<ScheduleCell>
{
    public AvailableScheduleCellSpecification() : base(sc =>
        (sc.Active && sc.Time >= TimeOnly.FromDateTime(DateTime.Now)) &&
        (sc.Meetups.Any(c => c.Date >= DateOnly.FromDateTime(DateTime.Now.Date))))
    {
    }
}
