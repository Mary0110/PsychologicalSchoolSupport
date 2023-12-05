namespace PsychologicalSupportPlatform.Common.Errors;

public class IsAlreadyApprovedException: Exception
{
    private const string Message = "The meetup is already approved";
    
    public IsAlreadyApprovedException()
        : base(Message)
    {
    }
}
