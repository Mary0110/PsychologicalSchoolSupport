using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Openings.Queries;

public class GetOpeningsByStatusQuery : IRequest<DataResponseInfo<List<OpeningDTO>>>
{
    public bool Active { get; set; }
}
