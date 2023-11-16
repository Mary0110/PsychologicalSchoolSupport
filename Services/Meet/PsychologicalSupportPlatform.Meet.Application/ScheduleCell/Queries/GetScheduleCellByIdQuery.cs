using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Queries;

public class GetScheduleCellByIdQuery : IRequest<DataResponseInfo<ScheduleCellDTO>>
{
    public int Id { get; set; }
}
