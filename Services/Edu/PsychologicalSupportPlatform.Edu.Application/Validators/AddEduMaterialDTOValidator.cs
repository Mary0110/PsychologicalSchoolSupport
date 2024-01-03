using FluentValidation;
using PsychologicalSupportPlatform.Edu.Application.DTOs;

namespace PsychologicalSupportPlatform.Edu.Application.Validators;

public class AddEduMaterialDTOValidator: AbstractValidator<AddEduMaterialDTO>
{
    public AddEduMaterialDTOValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty();
        RuleFor(dto => dto.Theme).NotEmpty();
    }
}
