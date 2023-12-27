using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs.ScheduleCell;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Commands.Update;

public record UpdateScheduleCellCommand(int Id, CreateScheduleCellDTO ScheduleCellDto): IRequest<int>;
