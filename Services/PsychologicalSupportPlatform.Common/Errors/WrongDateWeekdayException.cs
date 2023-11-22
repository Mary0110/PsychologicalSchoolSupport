namespace PsychologicalSupportPlatform.Common.Errors;

public class WrongDateWeekdayException: Exception
{
    private static string Message = "Weekday in date and schedule cell don't match";

    public WrongDateWeekdayException()
        : base(Message)
    {
    }
}
