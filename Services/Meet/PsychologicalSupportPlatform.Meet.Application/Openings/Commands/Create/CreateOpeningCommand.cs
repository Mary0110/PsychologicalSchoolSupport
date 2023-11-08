using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Openings.Commands.Create;

public record CreateOpeningCommand(AddCmdOpeningDTO OpeningDto): IRequest<ResponseInfo>;