namespace PsychologicalSupportPlatform.Authorization.Domain.Validators.StudentValidators;

public static class ValidatorHelper
{
    public static bool BeAValidAge(DateTime date)
    {
        var today = DateTime.Today;
        var age = today.Year - date.Year;
        return age >= 5;
    }
    
    public static bool BeAValidDate(DateTime date)
    {
        return !date.Equals(default);
    }
}
