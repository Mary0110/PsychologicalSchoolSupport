using MediatR;
using PsychologicalSupportPlatform.Common;

namespace PsychologicalSupportPlatform.Meet.Application.Openings.Commands.Delete;

public record DeleteOpeningCommand(int Id): IRequest<ResponseInfo>;