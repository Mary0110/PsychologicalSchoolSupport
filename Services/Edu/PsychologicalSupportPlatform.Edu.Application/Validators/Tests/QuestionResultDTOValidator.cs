using FluentValidation;
using PsychologicalSupportPlatform.Edu.Application.DTOs.Tests;

namespace PsychologicalSupportPlatform.Edu.Application.Validators.Tests;

public class QuestionResultDTOValidator: AbstractValidator<QuestionResultDTO>
{
    public QuestionResultDTOValidator()
    {
        RuleFor(dto => dto.AnswerId).NotEmpty().GreaterThan(0);
        
        RuleFor(dto => dto.QuestionId).NotEmpty().GreaterThan(0);
    }
}
