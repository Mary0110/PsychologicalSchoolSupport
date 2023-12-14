using FluentValidation;
using PsychologicalSupportPlatform.Edu.Application.DTOs;

namespace PsychologicalSupportPlatform.Edu.Application.Validators;

public class AddEduMaterialToStudentDTOValidator: AbstractValidator<AddEduMaterialToStudentDTO>
{
    public  AddEduMaterialToStudentDTOValidator()
    {
        RuleFor(dto => dto.Id).NotEmpty();
        RuleFor(dto => dto.StudentId).NotEmpty();
    }
}
