using FluentValidation;
using PsychologicalSupportPlatform.Meet.Application.DTOs.Meetup;

namespace PsychologicalSupportPlatform.Meet.Application.Validators.Meetup;

public class AddMeetupByStudentValidator: AbstractValidator<AddMeetupByStudentDTO>
{
    public AddMeetupByStudentValidator()
    {
        RuleFor(dto => dto.Date).NotEmpty()
            .Must(IsFutureDate).WithMessage("only future dates are allowed")
            .Must(IsNotSunday).WithMessage("Sunday is not working day");
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