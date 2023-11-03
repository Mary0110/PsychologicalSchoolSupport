using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Opening.Queries;

public class GetAllOpeningsQuery : IRequest<List<OpeningDTO>>
{
}