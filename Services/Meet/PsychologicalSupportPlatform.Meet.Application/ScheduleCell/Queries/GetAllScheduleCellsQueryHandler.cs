using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Queries;

public class GetAllScheduleCellsQueryHandler : IRequestHandler<GetAllScheduleCellsQuery, DataResponseInfo<List<ScheduleCellDTO>>>
{
    private readonly IScheduleCellRepository scheduleCellRepository;
    private readonly IMapper mapper;
    
    public GetAllScheduleCellsQueryHandler(IScheduleCellRepository scheduleCellRepository, IMapper mapper)
    {
        this.scheduleCellRepository = scheduleCellRepository;
        this.mapper = mapper;
    }

    public async Task<DataResponseInfo<List<ScheduleCellDTO>>> Handle(GetAllScheduleCellsQuery request, CancellationToken cancellationToken)
    {
        var scheduleCells = await scheduleCellRepository.GetAllScheduleCellsAsync();

        if (scheduleCells is null)
        {
            throw new EntityNotFoundException();
        }

        var scheduleCellModel = mapper.Map<List<ScheduleCellDTO>>(scheduleCells);
        
        return new DataResponseInfo<List<ScheduleCellDTO>>(data: scheduleCellModel, success: true, message: "all meetups");
    }
}
