using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace PsychologicalSupportPlatform.Meet.Infrastructure.Data;


public class TimeOnlyConverter : ValueConverter<TimeOnly, TimeSpan>
{
    public TimeOnlyConverter() : base(
        timeOnly => timeOnly.ToTimeSpan(), 
        timeSpan => TimeOnly.FromTimeSpan(timeSpan))
    { }
}