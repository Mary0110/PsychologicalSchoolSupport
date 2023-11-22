using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Queries;

public record GetAllScheduleCellsQuery(int pageNumber, int pageSize) : IRequest<List<ScheduleCellDTO>>
{
}
