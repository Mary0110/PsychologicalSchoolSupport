using FluentValidation;
using PsychologicalSupportPlatform.Edu.Application.DTOs.Tests;

namespace PsychologicalSupportPlatform.Edu.Application.Validators.Tests;

public class AddAnswerDTOValidator: AbstractValidator<AddAnswerDTO>
{
    public AddAnswerDTOValidator()
    {
        RuleFor(dto => dto.AnswerText).NotEmpty().MaximumLength(50);
    }
}
