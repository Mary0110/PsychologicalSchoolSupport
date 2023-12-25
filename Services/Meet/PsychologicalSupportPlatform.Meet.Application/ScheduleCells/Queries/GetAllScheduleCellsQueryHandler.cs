using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Application.DTOs.ScheduleCell;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Queries;

public class GetAllScheduleCellsQueryHandler : IRequestHandler<GetAllScheduleCellsQuery, List<ScheduleCellDTO>>
{
    private readonly IScheduleCellRepository scheduleCellRepository;
    private readonly IMapper mapper;
    
    public GetAllScheduleCellsQueryHandler(IScheduleCellRepository scheduleCellRepository, IMapper mapper)
    {
        this.scheduleCellRepository = scheduleCellRepository;
        this.mapper = mapper;
    }

    public async Task<List<ScheduleCellDTO>> Handle(GetAllScheduleCellsQuery request, CancellationToken cancellationToken)
    {
        var scheduleCells = await scheduleCellRepository.GetAllAsync(request.pageNumber, request.pageSize);

        if (scheduleCells is null)
        {
            throw new EntityNotFoundException();
        }

        var scheduleCellModel = mapper.Map<List<ScheduleCellDTO>>(scheduleCells);

        return scheduleCellModel;
    }
}
