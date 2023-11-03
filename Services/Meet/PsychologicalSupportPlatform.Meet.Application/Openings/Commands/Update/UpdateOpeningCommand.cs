using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs;

namespace PsychologicalSupportPlatform.Meet.Application.Openings.Commands.Update;

public record UpdateOpeningCommand(OpeningDTO openingDTO): IRequest;