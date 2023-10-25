using FluentValidation;
using PsychologicalSupportPlatform.Authorization.Domain.DTOs;

namespace PsychologicalSupportPlatform.Authorization.Domain.Validators.UserValidators;

public class AddUserValidator: AbstractValidator<AddUserDTO>
{
    public AddUserValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.Role).IsInEnum();
    }
}