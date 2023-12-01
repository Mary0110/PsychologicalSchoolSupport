using PsychologicalSupportPlatform.Common.Repository.Specifications;
using PsychologicalSupportPlatform.Meet.Domain.Entities;

namespace PsychologicalSupportPlatform.Meet.Infrastructure.Data.Specifications;

public class LastQuarterMeetupsOfPsychologist: Specification<Meetup>
{
    public LastQuarterMeetupsOfPsychologist(int psychologistId) : base(

        m => m.Date >= DateOnly.FromDateTime(DateTime.Today.AddMonths(-4)) &&
             m.ScheduleCell.PsychologistId == psychologistId)
    {
    }
}
