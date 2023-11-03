using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Opening.Queries;

public class GetOpeningByIdQuery : IRequest<OpeningDTO>
{
    public int Id { get; set; }
}