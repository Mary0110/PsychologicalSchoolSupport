namespace PsychologicalSupportPlatform.Common.Errors;

public class DocNotCreatedException : Exception
{
    private const string Message = "Elk document wasn't created";

    public DocNotCreatedException()
        : base(Message)
    {
    }
}
