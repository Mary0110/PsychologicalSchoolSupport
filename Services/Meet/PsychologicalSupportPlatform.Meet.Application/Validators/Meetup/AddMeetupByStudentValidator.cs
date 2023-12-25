using FluentValidation;
using PsychologicalSupportPlatform.Meet.Application.DTOs.Meetup;

namespace PsychologicalSupportPlatform.Meet.Application.Validators.Meetup;

public class AddMeetupByStudentValidator: AbstractValidator<AddMeetupByStudentDTO>
{
    public AddMeetupByStudentValidator()
    {
        RuleFor(dto => dto.Date).NotEmpty()
            .Must(MeetupValidatorHelper.IsFutureDate).WithMessage("only future dates are allowed")
            .Must(MeetupValidatorHelper.IsNotSunday).WithMessage("Sunday is not working day");
        RuleFor(dto => dto.ScheduleCellId).NotEmpty().GreaterThanOrEqualTo(0);
    }
}
