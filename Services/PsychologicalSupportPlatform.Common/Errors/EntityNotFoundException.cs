namespace PsychologicalSupportPlatform.Common.Errors;

public class EntityNotFoundException : ArgumentException
{
    private const string EntityNotFoundMessage = "Entity not found";

    public EntityNotFoundException(string paramname)
        : base(EntityNotFoundMessage, paramname)
    {
    }
    
    public EntityNotFoundException()
    {
    }
}
