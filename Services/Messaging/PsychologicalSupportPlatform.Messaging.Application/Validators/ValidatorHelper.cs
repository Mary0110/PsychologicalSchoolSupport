namespace PsychologicalSupportPlatform.Messaging.Application.Validators;

public static class ValidatorHelper
{
    public static bool IsValidId(string id)
    {
        return int.Parse(id) >= 0;
    }
}
