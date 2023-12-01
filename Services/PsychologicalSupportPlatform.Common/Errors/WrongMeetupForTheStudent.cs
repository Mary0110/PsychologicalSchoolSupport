namespace PsychologicalSupportPlatform.Common.Errors;

public class WrongMeetupForTheStudent: Exception
{
    private const string Message = "The student can't approve the meetup";
    
    public WrongMeetupForTheStudent()
        : base(Message)
    {
    }
}
