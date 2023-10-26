using FluentValidation;
using PsychologicalSupportPlatform.Authorization.Domain.DTOs;

namespace PsychologicalSupportPlatform.Authorization.Domain.Validators.StudentValidators;

public class AddStudentValidator : AbstractValidator<AddStudentDTO>
{
    public AddStudentValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage(Constants.mesDayOfBirthRequired)
            .Must(BeAValidDate).WithMessage(Constants.mesDayOfBirthValid)
            .Must(BeAValidAge).WithMessage(Constants.mesDayOfBirthGreater);
        RuleFor(x => x.Parallel).NotEmpty().InclusiveBetween(1, 11).NotEmpty();
        RuleFor(x => x.Letter).NotEmpty().Must(c => c >= Constants.A && c <= Constants.Z);
    }

    private bool BeAValidDate(DateTime date)
    {
        return !date.Equals(default);
    }
    
    private bool BeAValidAge(DateTime date)
    {
        var today = DateTime.Today;
        var age = today.Year - date.Year;
        return age >= 5;
    }
}
