using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs.ScheduleCell;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Queries;

public record GetScheduleCellsByDayOfWeekQuery(DayOfWeek DayOfWeek, int PageNumber, int PageSize) :
    IRequest<List<ScheduleCellDTO>>;
