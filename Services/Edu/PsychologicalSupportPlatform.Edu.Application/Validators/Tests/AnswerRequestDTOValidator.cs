using FluentValidation;
using PsychologicalSupportPlatform.Edu.Application.DTOs.Tests;

namespace PsychologicalSupportPlatform.Edu.Application.Validators.Tests;

public class AnswerRequestDTOValidator: AbstractValidator<AnswerRequestDTO>
{
    public AnswerRequestDTOValidator()
    {
        RuleFor(dto => dto.TestId).NotEmpty().GreaterThan(0);
        RuleFor(dto => dto.QuestionResultDTOs).NotNull();
    }
}
