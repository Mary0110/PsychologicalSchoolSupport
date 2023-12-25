using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Application.DTOs.ScheduleCell;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Queries;

public record GetScheduleCellsByStatusQuery(bool Active, int pageNumber, int pageSize) : IRequest<List<ScheduleCellDTO>>
{ }
