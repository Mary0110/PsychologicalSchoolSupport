using FluentValidation;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Application.DTOs.ScheduleCell;

namespace PsychologicalSupportPlatform.Meet.Application.Validators.ScheduleCell;

public class AddScheduleCellValidator : AbstractValidator<AddScheduleCellDTO>
{
    public AddScheduleCellValidator()
    {
        RuleFor(dto => dto.Active).NotEmpty();
        RuleFor(dto => dto.Day).NotEmpty();
        RuleFor(dto => dto.Time).NotEmpty()
            .LessThanOrEqualTo(TimeOnly.MaxValue)
            .GreaterThanOrEqualTo(TimeOnly.MinValue);
    }
}
