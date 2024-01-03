using FluentValidation;
using PsychologicalSupportPlatform.Report.Application.DTOs;

namespace PsychologicalSupportPlatform.Report.Application.Validators;

public class GetMonthlyReportDTOValidator: AbstractValidator<GetMonthlyReportDTO>
{
    public GetMonthlyReportDTOValidator()
    {
        RuleFor(dto => dto.Month).NotEmpty().InclusiveBetween(1, 12);
    }
}
