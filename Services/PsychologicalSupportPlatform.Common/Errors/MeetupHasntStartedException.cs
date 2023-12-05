namespace PsychologicalSupportPlatform.Common.Errors;

public class MeetupHasntStartedException: Exception
{
    private const string Message = "The meetup hasn't started to be approved yet";
    
    public MeetupHasntStartedException()
        : base(Message)
    {
    }
}
