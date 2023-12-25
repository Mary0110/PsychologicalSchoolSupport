using FluentValidation;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Application.DTOs.Meetup;

namespace PsychologicalSupportPlatform.Meet.Application.Validators.Meetup;

public class AddMeetupValidator : AbstractValidator<AddMeetupDTO>
{
    public AddMeetupValidator()
    {
        RuleFor(dto => dto.Date).NotEmpty()
            .Must(MeetupValidatorHelper.IsFutureDate).WithMessage("only future dates are allowed")
            .Must(MeetupValidatorHelper.IsNotSunday).WithMessage("Sunday is not working day");
        RuleFor(dto => dto.StudentId).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(dto => dto.ScheduleCellId).NotEmpty().GreaterThanOrEqualTo(0);
    }
}
