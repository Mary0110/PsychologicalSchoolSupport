using FluentValidation;
using PsychologicalSupportPlatform.Edu.Application.DTOs;

namespace PsychologicalSupportPlatform.Edu.Application.Validators;

public class EduMaterialDTOValidator: AbstractValidator<EduMaterialDTO>
{
    public EduMaterialDTOValidator()
    {
        RuleFor(dto => dto.Id).NotEmpty().GreaterThan(0);
        
        RuleFor(dto => dto.Name).NotEmpty().MaximumLength(50);
        
        RuleFor(dto => dto.Theme).NotEmpty().MaximumLength(50);
    }
}
