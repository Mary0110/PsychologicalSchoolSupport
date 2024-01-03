namespace PsychologicalSupportPlatform.Meet.Application.Validators.Meetup;

public static class MeetupValidatorHelper
{
    public static bool IsNotSunday(DateOnly date)
    {
        return !(date.DayOfWeek is DayOfWeek.Sunday);
    }
    
    public static bool IsFutureDate(DateOnly date)
    {
        return date >= DateOnly.FromDateTime(DateTime.Now);
    }
}
