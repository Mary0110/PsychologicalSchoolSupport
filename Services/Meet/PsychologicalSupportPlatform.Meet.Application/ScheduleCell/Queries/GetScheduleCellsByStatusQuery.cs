using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Queries;

public class GetScheduleCellsByStatusQuery : IRequest<DataResponseInfo<List<ScheduleCellDTO>>>
{
    public bool Active { get; set; }
}
