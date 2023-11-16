using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Queries;

public class GetScheduleCellsByStatusHandler : IRequestHandler<GetScheduleCellsByStatusQuery, DataResponseInfo<List<ScheduleCellDTO>>>
{
    private readonly IScheduleCellRepository scheduleCellRepository;
    private readonly IMapper mapper;

    public GetScheduleCellsByStatusHandler(IScheduleCellRepository scheduleCellRepository, IMapper mapper)
    {
        this.scheduleCellRepository = scheduleCellRepository;
        this.mapper = mapper;
    }
    
    public async Task<DataResponseInfo<List<ScheduleCellDTO>>> Handle(GetScheduleCellsByStatusQuery request, CancellationToken cancellationToken)
    {
        var scheduleCells = await scheduleCellRepository.GetScheduleCellsByStatusAsync(request.Active);

        if (scheduleCells is null)
        {
            throw new EntityNotFoundException(nameof(request.Active));
        }

        var scheduleCellModel = mapper.Map<List<ScheduleCellDTO>>(scheduleCells);
        
        return new DataResponseInfo<List<ScheduleCellDTO>>(data: scheduleCellModel, success: true, message: $"no scheduleCells with active status {request.Active}");
    }
}
