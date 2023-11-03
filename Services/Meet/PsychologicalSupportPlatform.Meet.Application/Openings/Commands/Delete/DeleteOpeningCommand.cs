using MediatR;

namespace PsychologicalSupportPlatform.Meet.Application.Openings.Commands.Delete;

public record DeleteOpeningCommand(int Id): IRequest;