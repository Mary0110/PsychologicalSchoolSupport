using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Queries;

public class GetScheduleCellsByDayOfWeekQuery : IRequest<DataResponseInfo<List<ScheduleCellDTO>>>
{
    public DayOfWeek DayOfWeek { get; set; }
}
