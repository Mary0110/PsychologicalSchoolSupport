namespace PsychologicalSupportPlatform.Common.Errors;

public class AlreadyExistsException: ArgumentException
{
    private const string Message = "Entity already exists";

    public AlreadyExistsException()
        : base(Message)
    {
    }
}