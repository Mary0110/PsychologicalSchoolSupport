using FluentValidation;
using PsychologicalSupportPlatform.Authorization.Domain.DTOs;
using PsychologicalSupportPlatform.Common;

namespace PsychologicalSupportPlatform.Authorization.Domain.Validators.UserValidators;

public class AddUserValidator: AbstractValidator<AddUserDTO>
{
    public AddUserValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.Role).Must(x => IsInRoles(x));
    }
    
    private bool IsInRoles(string role)
    {
        return new[] { Roles.Admin, Roles.Manager, Roles.Psychologist, Roles.Student }.Contains(role);
    }
}