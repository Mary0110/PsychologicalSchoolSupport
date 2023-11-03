using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Opening.Queries;

public class GetOpeningsByStatusQuery : IRequest<List<OpeningDTO>>
{
    public bool Active { get; set; }
}