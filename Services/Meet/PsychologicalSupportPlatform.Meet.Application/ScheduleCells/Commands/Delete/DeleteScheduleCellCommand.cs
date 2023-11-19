using MediatR;
using PsychologicalSupportPlatform.Common;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Commands.Delete;

public record DeleteScheduleCellCommand(int Id): IRequest<int>;