using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Application.DTOs.ScheduleCell;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Queries;

public class GetScheduleCellsByDayOfWeekHandler : IRequestHandler<GetScheduleCellsByDayOfWeekQuery, List<ScheduleCellDTO>>
{
    private readonly IScheduleCellRepository scheduleCellRepository;
    private readonly IMapper mapper;

    public GetScheduleCellsByDayOfWeekHandler(IScheduleCellRepository scheduleCellRepository, IMapper mapper)
    {
        this.scheduleCellRepository = scheduleCellRepository;
        this.mapper = mapper;
    }

    public async Task<List<ScheduleCellDTO>> Handle(GetScheduleCellsByDayOfWeekQuery request,
        CancellationToken cancellationToken)
    {
        var scheduleCells =
            await scheduleCellRepository.GetScheduleCellsByDayOfWeekAsync(request.DayOfWeek, request.pageNumber,
                request.pageSize);

        if (scheduleCells is null)
        {
            throw new EntityNotFoundException(nameof(request.DayOfWeek));
        }

        var scheduleCellModel = mapper.Map<List<ScheduleCellDTO>>(scheduleCells);

        return scheduleCellModel;
    }
}
