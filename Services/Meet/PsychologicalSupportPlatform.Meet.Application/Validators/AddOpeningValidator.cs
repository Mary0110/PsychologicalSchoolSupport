using FluentValidation;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Validators;

public class AddOpeningValidator: AbstractValidator<AddOpeningDTO>
{
    public AddOpeningValidator()
    {
        RuleFor(x => x.Day).NotEqual(DayOfWeek.Sunday).NotEmpty();
        RuleFor(x => x.Hours).LessThanOrEqualTo(18).GreaterThanOrEqualTo(8).NotEmpty();
        RuleFor(x => x.Minutes).LessThanOrEqualTo(59).GreaterThanOrEqualTo(0).NotEmpty();
        RuleFor(x => x.PsychologistId).NotEmpty();
        RuleFor(x => x.Active).NotEmpty();
    }
}
