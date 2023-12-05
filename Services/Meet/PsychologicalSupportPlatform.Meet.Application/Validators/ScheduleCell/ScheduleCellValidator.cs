using FluentValidation;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Validators.ScheduleCell;

public class ScheduleCellValidator: AbstractValidator<ScheduleCellDTO>
{
    public ScheduleCellValidator()
    {
        RuleFor(dto => dto.Active).NotEmpty();
        RuleFor(dto => dto.Day).NotEmpty();
        RuleFor(dto => dto.Hours).NotEmpty()
            .LessThanOrEqualTo(TimeOnly.MaxValue.Hour)
            .GreaterThanOrEqualTo(TimeOnly.MinValue.Hour);
        RuleFor(dto => dto.Hours).NotEmpty()
            .LessThanOrEqualTo(TimeOnly.MaxValue.Minute)
            .GreaterThanOrEqualTo(TimeOnly.MinValue.Minute);
        RuleFor(dto => dto.PsychologistId).GreaterThan(0);
    }
}
