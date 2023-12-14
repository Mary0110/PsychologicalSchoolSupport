namespace PsychologicalSupportPlatform.Common.Errors;

public class NotAllowedActionForTheUserException: InvalidOperationException
{
    public NotAllowedActionForTheUserException(int id)
        : base($"Not allowed for user with id {id}")
    {
    }
}
