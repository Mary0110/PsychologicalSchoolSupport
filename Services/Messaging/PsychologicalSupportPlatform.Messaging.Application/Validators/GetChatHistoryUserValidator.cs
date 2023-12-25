using FluentValidation;
using PsychologicalSupportPlatform.Messaging.Application.DTOs;

namespace PsychologicalSupportPlatform.Messaging.Application.Validators;

public class GetChatHistoryValidator: AbstractValidator<GetChatHistoryDTO>
{
    public GetChatHistoryValidator()
    {
        RuleFor(dto => dto.OtherUserId).NotEmpty().Must(ValidatorHelper.IsValidId);
        RuleFor(dto => dto.SenderId).NotEmpty().Must(ValidatorHelper.IsValidId);
    }
}
