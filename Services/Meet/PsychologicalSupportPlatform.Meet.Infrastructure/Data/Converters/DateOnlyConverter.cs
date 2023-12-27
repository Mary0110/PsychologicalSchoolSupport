using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace PsychologicalSupportPlatform.Meet.Infrastructure.Data.Converters;

public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyConverter() : base(
        dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue), 
        dateTime => DateOnly.FromDateTime(dateTime))
    { }
}
