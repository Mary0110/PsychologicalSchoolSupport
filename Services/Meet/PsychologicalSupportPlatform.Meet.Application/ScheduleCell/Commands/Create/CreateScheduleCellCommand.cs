using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Commands.Create;

public record CreateScheduleCellCommand(AddCmdScheduleCellDTO ScheduleCellDto): IRequest<ResponseInfo>;