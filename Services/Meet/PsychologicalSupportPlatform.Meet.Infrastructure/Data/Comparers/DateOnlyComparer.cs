using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PsychologicalSupportPlatform.Meet.Infrastructure.Data.Comparers;


public class DateOnlyComparer : ValueComparer<DateOnly>
{
    public DateOnlyComparer() : base(
        (x, y) => x.DayNumber == y.DayNumber,
        dateOnly => dateOnly.GetHashCode())
    { }
}
