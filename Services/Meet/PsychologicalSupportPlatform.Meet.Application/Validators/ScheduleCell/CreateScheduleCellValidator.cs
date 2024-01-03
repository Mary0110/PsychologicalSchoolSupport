using FluentValidation;
using PsychologicalSupportPlatform.Meet.Application.DTOs.ScheduleCell;

namespace PsychologicalSupportPlatform.Meet.Application.Validators.ScheduleCell;

public class CreateScheduleCellValidator: AbstractValidator<CreateScheduleCellDTO>
{
    public CreateScheduleCellValidator()
    {
        RuleFor(dto => dto.Active).NotEmpty();
        
        RuleFor(dto => dto.Day).NotEmpty().NotEqual(DayOfWeek.Sunday);
        
        RuleFor(dto => dto.Hours).NotEmpty()
            .LessThanOrEqualTo(20)
            .GreaterThanOrEqualTo(8);
        
        RuleFor(dto => dto.Hours).NotEmpty()
            .LessThanOrEqualTo(TimeOnly.MaxValue.Minute)
            .GreaterThanOrEqualTo(TimeOnly.MinValue.Minute);
    }
}
