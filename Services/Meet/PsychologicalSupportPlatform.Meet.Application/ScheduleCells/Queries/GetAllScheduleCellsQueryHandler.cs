using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Application.DTOs.ScheduleCell;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Queries;

public class GetAllScheduleCellsQueryHandler : IRequestHandler<GetAllScheduleCellsQuery, List<ScheduleCellDTO>>
{
    private readonly IScheduleCellRepository _scheduleCellRepository;
    private readonly IMapper _mapper;
    
    public GetAllScheduleCellsQueryHandler(IScheduleCellRepository scheduleCellRepository, IMapper mapper)
    {
        _scheduleCellRepository = scheduleCellRepository;
        _mapper = mapper;
    }

    public async Task<List<ScheduleCellDTO>> Handle(GetAllScheduleCellsQuery request, CancellationToken cancellationToken)
    {
        var scheduleCells = await _scheduleCellRepository.GetAllAsync(request.PageNumber, request.PageSize);

        if (scheduleCells is null)
        {
            throw new EntityNotFoundException();
        }

        var scheduleCellModel = _mapper.Map<List<ScheduleCellDTO>>(scheduleCells);

        return scheduleCellModel;
    }
}
