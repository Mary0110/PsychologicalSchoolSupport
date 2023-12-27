using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs.ScheduleCell;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Queries;

public class GetScheduleCellByIdQuery : IRequest<ScheduleCellDTO>
{
    public int Id { get; set; }
}
