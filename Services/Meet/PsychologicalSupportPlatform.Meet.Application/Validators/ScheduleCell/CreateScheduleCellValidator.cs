using FluentValidation;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Validators.ScheduleCell;

public class CreateScheduleCellValidator: AbstractValidator<CreateScheduleCellDTO>
{
    public CreateScheduleCellValidator()
    {
        RuleFor(dto => dto.Active).NotEmpty();
        RuleFor(dto => dto.Day).NotEmpty();
        RuleFor(dto => dto.Hours).NotEmpty()
            .LessThanOrEqualTo(TimeOnly.MaxValue.Hour)
            .GreaterThanOrEqualTo(TimeOnly.MinValue.Hour);
        
        RuleFor(dto => dto.Hours).NotEmpty()
            .LessThanOrEqualTo(TimeOnly.MaxValue.Minute)
            .GreaterThanOrEqualTo(TimeOnly.MinValue.Minute);
    }
}
