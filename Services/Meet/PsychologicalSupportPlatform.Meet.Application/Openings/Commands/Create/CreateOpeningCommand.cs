using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Openings.Commands.Create;

public record CreateOpeningCommand(AddOpeningDTO OpeningDto): IRequest<int>;