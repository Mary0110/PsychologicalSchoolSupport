using FluentValidation;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Validators.Meetup;

public class AddMeetupValidator : AbstractValidator<AddMeetupDTO>
{
    public AddMeetupValidator()
    {
        RuleFor(dto => dto.Date).NotEmpty()
            .Must(IsFutureDate).WithMessage("only future dates are allowed")
            .Must(IsNotSunday).WithMessage("Sunday is not working day");
        RuleFor(dto => dto.StudentId).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(dto => dto.ScheduleCellId).NotEmpty().GreaterThanOrEqualTo(0);
    }

    private static bool IsNotSunday(DateOnly date)
    {
        return !(date.DayOfWeek is DayOfWeek.Sunday);
    }
    
    private static bool IsFutureDate(DateOnly date)
    {
        return date > DateOnly.FromDateTime(DateTime.Now);
    }
}