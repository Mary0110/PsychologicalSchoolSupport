using FluentValidation;
using PsychologicalSupportPlatform.Authorization.Domain.DTOs;

namespace PsychologicalSupportPlatform.Authorization.Domain.Validators.FormValidators;

public class AddFormValidator : AbstractValidator<AddFormDTO>
{
    public AddFormValidator()
    {
        RuleFor(x => x.Parallel).InclusiveBetween(1,11).NotEmpty();;
        RuleFor(x => x.Letter).NotEmpty().Must(c => c >= Constants.A && c <= Constants.Z);
    }
}
