using FluentValidation;
using PsychologicalSupportPlatform.Edu.Application.DTOs.Tests;

namespace PsychologicalSupportPlatform.Edu.Application.Validators.Tests;

public class AddQuestionDTOValidator: AbstractValidator<AddQuestionDTO>
{
    public AddQuestionDTOValidator()
    {
        RuleFor(dto => dto.Text).NotEmpty().MaximumLength(100);
        RuleFor(dto => dto.Answers).NotEmpty();
    }
}
