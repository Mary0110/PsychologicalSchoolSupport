using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Opening.Queries;

public class GetOpeningsByDayOfWeekQuery : IRequest<List<OpeningDTO>>
{
    public DayOfWeek DayOfWeek { get; set; }
}