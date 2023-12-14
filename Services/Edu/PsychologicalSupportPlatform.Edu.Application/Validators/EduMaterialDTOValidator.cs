using FluentValidation;
using PsychologicalSupportPlatform.Edu.Application.DTOs;

namespace PsychologicalSupportPlatform.Edu.Application.Validators;

public class EduMaterialDTOValidator: AbstractValidator<EduMaterialDTO>
{
    public EduMaterialDTOValidator()
    {
        RuleFor(dto => dto.Id).NotEmpty();
        RuleFor(dto => dto.Name).NotEmpty();
        RuleFor(dto => dto.Theme).NotEmpty();
    }
}
