using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Queries;

public class GetScheduleCellByIdHandler : IRequestHandler<GetScheduleCellByIdQuery, DataResponseInfo<ScheduleCellDTO>>
{
    private readonly IScheduleCellRepository scheduleCellRepository;
    private readonly IMapper mapper;

    public GetScheduleCellByIdHandler(IScheduleCellRepository scheduleCellRepository, IMapper mapper)
    {
        this.scheduleCellRepository = scheduleCellRepository;
        this.mapper = mapper;
    }
    
    public async Task<DataResponseInfo<ScheduleCellDTO>> Handle(GetScheduleCellByIdQuery request, CancellationToken cancellationToken)
    {
        var scheduleCell = await scheduleCellRepository.GetScheduleCellByIdAsync(request.Id);

        if (scheduleCell is null)
        {
            throw new EntityNotFoundException(nameof(request.Id));
        }

        var scheduleCellModel = mapper.Map<ScheduleCellDTO>(scheduleCell);
        
        return new DataResponseInfo<ScheduleCellDTO>(data: scheduleCellModel, success: true, message: $"scheduleCell with id {request.Id}");  
    }
}
