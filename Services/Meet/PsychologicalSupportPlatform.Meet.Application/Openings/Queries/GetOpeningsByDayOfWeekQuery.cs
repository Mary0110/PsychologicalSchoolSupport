using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Openings.Queries;

public class GetOpeningsByDayOfWeekQuery : IRequest<DataResponseInfo<List<OpeningDTO>>>
{
    public DayOfWeek DayOfWeek { get; set; }
}
