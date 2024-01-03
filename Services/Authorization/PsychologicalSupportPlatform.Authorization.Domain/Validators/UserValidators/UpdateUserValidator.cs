using FluentValidation;
using PsychologicalSupportPlatform.Authorization.Domain.DTOs;

namespace PsychologicalSupportPlatform.Authorization.Domain.Validators.UserValidators;

public class UpdateUserValidator : AbstractValidator<UpdateUserDTO>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        
        RuleFor(x => x.Surname).NotEmpty();
        
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
        
        RuleFor(x => x.Password).NotEmpty();
    }
}
