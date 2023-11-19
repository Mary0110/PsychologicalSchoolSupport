namespace PsychologicalSupportPlatform.Common.Errors;

public class WrongRequestDataException : ArgumentException
{
    private const string EntityNotFoundMessage = "Wrong request data";

    public WrongRequestDataException()
        : base(EntityNotFoundMessage)
    {
    }
}
