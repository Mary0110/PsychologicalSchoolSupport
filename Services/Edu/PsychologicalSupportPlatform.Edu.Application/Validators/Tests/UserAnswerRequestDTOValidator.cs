using FluentValidation;
using PsychologicalSupportPlatform.Edu.Application.DTOs.Tests;

namespace PsychologicalSupportPlatform.Edu.Application.Validators.Tests;

public class UserAnswerRequestDTOValidator: AbstractValidator<UserAnswerRequestDTO>
{
    public UserAnswerRequestDTOValidator()
    {
        RuleFor(dto => dto.UserId).NotEmpty();
        
        RuleFor(dto => dto.AnswerRequestDTO).NotNull();
    }
}
