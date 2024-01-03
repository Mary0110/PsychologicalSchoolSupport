using FluentValidation;
using PsychologicalSupportPlatform.Edu.Application.DTOs.Tests;

namespace PsychologicalSupportPlatform.Edu.Application.Validators.Tests;

public class AddTestDTOValidator: AbstractValidator<AddTetsDTO>
{
    public AddTestDTOValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty().MaximumLength(50);
        
        RuleFor(dto => dto.Questions).NotEmpty();
    }
}
