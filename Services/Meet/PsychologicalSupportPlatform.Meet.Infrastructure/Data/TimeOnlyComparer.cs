using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PsychologicalSupportPlatform.Meet.Infrastructure.Data;


public class TimeOnlyComparer : ValueComparer<TimeOnly>
{
    public TimeOnlyComparer() : base(
        (x, y) => x.Ticks == y.Ticks,
        timeOnly => timeOnly.GetHashCode())
    { }
}