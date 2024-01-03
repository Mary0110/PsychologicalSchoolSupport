using FluentValidation;
using PsychologicalSupportPlatform.Messaging.Application.DTOs;

namespace PsychologicalSupportPlatform.Messaging.Application.Validators;

public class AddMessageValidator: AbstractValidator<AddMessageDTO>
{
    public AddMessageValidator()
    {
        RuleFor(dto => dto.Text).NotEmpty();
        
        RuleFor(dto => dto.ConsumerId).NotEmpty().Must(ValidatorHelper.IsValidId);
        
        RuleFor(dto => dto.DateTime).NotEmpty().GreaterThanOrEqualTo(DateTime.Now);
        
        RuleFor(dto => dto.SenderId).NotEmpty().Must(ValidatorHelper.IsValidId);
    }
}
