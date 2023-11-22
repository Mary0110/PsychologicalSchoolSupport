using FluentValidation;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Validators.ScheduleCell;

public class UpdateScheduleCellValidator: AbstractValidator<UpdateScheduleCellDTO>
{
    public UpdateScheduleCellValidator()
    {
        RuleFor(dto => dto.Id).GreaterThan(0);
        RuleFor(dto => dto.Active).NotEmpty();
        RuleFor(dto => dto.Day).NotEmpty();
        RuleFor(dto => dto.Time).NotEmpty()
            .LessThanOrEqualTo(TimeOnly.MaxValue)
            .GreaterThanOrEqualTo(TimeOnly.MinValue);
        RuleFor(dto => dto.PsychologistId).GreaterThan(0);
    }
}
