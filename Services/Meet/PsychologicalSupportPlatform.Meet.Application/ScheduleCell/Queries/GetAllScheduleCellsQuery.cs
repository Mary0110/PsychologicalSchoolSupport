using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Queries;

public class GetAllScheduleCellsQuery : IRequest<DataResponseInfo<List<ScheduleCellDTO>>>
{
}
