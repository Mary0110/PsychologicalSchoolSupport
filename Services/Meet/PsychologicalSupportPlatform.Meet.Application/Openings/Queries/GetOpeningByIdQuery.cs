using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Openings.Queries;

public class GetOpeningByIdQuery : IRequest<DataResponseInfo<OpeningDTO>>
{
    public int Id { get; set; }
}
