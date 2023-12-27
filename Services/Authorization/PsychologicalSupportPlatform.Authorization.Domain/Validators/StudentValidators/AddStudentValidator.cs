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
        
        RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage(Constants.MesDayOfBirthRequired)
            .Must(ValidatorHelper.BeAValidDate).WithMessage(Constants.MesDayOfBirthValid)
            .Must(ValidatorHelper.BeAValidAge).WithMessage(Constants.MesDayOfBirthGreater);
        
        RuleFor(x => x.Parallel).NotEmpty().InclusiveBetween(1, 11).NotEmpty();
        
        RuleFor(x => x.Letter).NotEmpty().Must(c => c >= Constants.A && c <= Constants.Z);
    }
}
