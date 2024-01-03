using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Application.DTOs.ScheduleCell;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Queries;

public class GetAvailableScheduleCellsQueryHandler: IRequestHandler<GetAvailableScheduleCellsQuery, List<ScheduleCellDTO>>
{
    private readonly IScheduleCellRepository _scheduleCellRepository;
    private readonly IMapper _mapper;

    public GetAvailableScheduleCellsQueryHandler(IScheduleCellRepository scheduleCellRepository, IMapper mapper)
    {
        _scheduleCellRepository = scheduleCellRepository;
        _mapper = mapper;
    }

    public async Task<List<ScheduleCellDTO>> Handle(GetAvailableScheduleCellsQuery request,
        CancellationToken cancellationToken)
    {
        var scheduleCells =
            await _scheduleCellRepository.GetAvailableScheduleCellsAsync(request.PageNumber, request.PageSize);

        if (scheduleCells is null)
        {
            throw new EntityNotFoundException();
        }

        var scheduleCellModel = _mapper.Map<List<ScheduleCellDTO>>(scheduleCells);

        return scheduleCellModel;
    }
}
