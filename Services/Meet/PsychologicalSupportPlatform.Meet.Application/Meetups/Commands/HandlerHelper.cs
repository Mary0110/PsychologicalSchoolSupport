using PsychologicalSupportPlatform.Meet.Domain.Entities;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands;

public static class HandlerHelper
{
    public static bool IsScheduleCellAvailable(ScheduleCell scheduleCell)
    {
        var hasOrderedMeetups = scheduleCell.Meetups.Any(m => m.Date > DateOnly.FromDateTime(DateTime.Today));
        
        if (!hasOrderedMeetups)
        {
            var hasOrderedMeetupsToday = scheduleCell.Meetups.Any(m => 
                m.Date == DateOnly.FromDateTime(DateTime.Now));
            
            if (hasOrderedMeetupsToday)
            {
                hasOrderedMeetups = scheduleCell.Time >= TimeOnly.FromDateTime(DateTime.Now);
            }
        }

        return scheduleCell.Active && !hasOrderedMeetups;
    }
}