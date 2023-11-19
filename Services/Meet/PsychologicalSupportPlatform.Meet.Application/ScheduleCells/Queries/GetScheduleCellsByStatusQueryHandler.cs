using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Queries;

public class GetScheduleCellsByStatusHandler : IRequestHandler<GetScheduleCellsByStatusQuery, List<ScheduleCellDTO>>
{
    private readonly IScheduleCellRepository scheduleCellRepository;
    private readonly IMapper mapper;

    public GetScheduleCellsByStatusHandler(IScheduleCellRepository scheduleCellRepository, IMapper mapper)
    {
        this.scheduleCellRepository = scheduleCellRepository;
        this.mapper = mapper;
    }
    
    public async Task<List<ScheduleCellDTO>> Handle(GetScheduleCellsByStatusQuery request, CancellationToken cancellationToken)
    {
        var scheduleCells = await scheduleCellRepository.GetScheduleCellsByStatusAsync(request.Active, request.pageNumber, request.pageSize);

        if (scheduleCells is null)
        {
            throw new EntityNotFoundException(nameof(request.Active));
        }

        var scheduleCellModel = mapper.Map<List<ScheduleCellDTO>>(scheduleCells);

        return scheduleCellModel;
    }
}
