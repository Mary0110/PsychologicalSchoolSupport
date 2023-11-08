using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Openings.Queries;

public class GetAvailableOpeningsQuery: IRequest<DataResponseInfo<List<OpeningDTO>>>
{
}
