using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Application.DTOs.ScheduleCell;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Queries;

public class GetScheduleCellsByDayOfWeekHandler : IRequestHandler<GetScheduleCellsByDayOfWeekQuery, List<ScheduleCellDTO>>
{
    private readonly IScheduleCellRepository _scheduleCellRepository;
    private readonly IMapper _mapper;

    public GetScheduleCellsByDayOfWeekHandler(IScheduleCellRepository scheduleCellRepository, IMapper mapper)
    {
        _scheduleCellRepository = scheduleCellRepository;
        _mapper = mapper;
    }

    public async Task<List<ScheduleCellDTO>> Handle(GetScheduleCellsByDayOfWeekQuery request,
        CancellationToken cancellationToken)
    {
        var scheduleCells =
            await _scheduleCellRepository.GetScheduleCellsByDayOfWeekAsync(request.DayOfWeek, request.PageNumber,
                request.PageSize);

        if (scheduleCells is null)
        {
            throw new EntityNotFoundException(nameof(request.DayOfWeek));
        }

        var scheduleCellModel = _mapper.Map<List<ScheduleCellDTO>>(scheduleCells);

        return scheduleCellModel;
    }
}
