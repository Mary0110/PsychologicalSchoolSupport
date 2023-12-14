namespace PsychologicalSupportPlatform.Common.Errors;

public class WrongRoleForActionRequested :InvalidOperationException
{
    public WrongRoleForActionRequested(string role)
        : base($"Invalid role: {role}")
    {
    }
}
