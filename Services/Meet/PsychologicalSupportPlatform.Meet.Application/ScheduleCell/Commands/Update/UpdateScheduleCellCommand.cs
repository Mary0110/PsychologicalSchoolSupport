using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Commands.Update;

public record UpdateScheduleCellCommand(ScheduleCellDTO scheduleCellDTO): IRequest<ResponseInfo>;
