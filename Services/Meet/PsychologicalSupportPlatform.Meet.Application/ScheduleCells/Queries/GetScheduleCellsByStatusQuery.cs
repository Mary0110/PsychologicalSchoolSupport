using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs.ScheduleCell;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Queries;

public record GetScheduleCellsByStatusQuery(bool Active, int PageNumber, int PageSize) : IRequest<List<ScheduleCellDTO>>
{ }
