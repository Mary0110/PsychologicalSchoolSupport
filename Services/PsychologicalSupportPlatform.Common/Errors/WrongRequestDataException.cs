namespace PsychologicalSupportPlatform.Common.Errors;

public class WrongRequestDataException : ArgumentException
{
    private const string Message = "Wrong request data";

    public WrongRequestDataException()
        : base(Message)
    {
    }
    
    public WrongRequestDataException(string paramname)
        : base(Message, paramname)
    {
    }
}
