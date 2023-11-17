using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Queries;

public record GetScheduleCellsByStatusQuery(bool Active, int pageNumber, int pageSize) : IRequest<List<ScheduleCellDTO>>
{ }
