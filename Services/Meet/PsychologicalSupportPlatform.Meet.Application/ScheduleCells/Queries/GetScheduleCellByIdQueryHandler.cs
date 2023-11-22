using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Queries;

public class GetScheduleCellByIdHandler : IRequestHandler<GetScheduleCellByIdQuery, ScheduleCellDTO>
{
    private readonly IScheduleCellRepository scheduleCellRepository;
    private readonly IMapper mapper;

    public GetScheduleCellByIdHandler(IScheduleCellRepository scheduleCellRepository, IMapper mapper)
    {
        this.scheduleCellRepository = scheduleCellRepository;
        this.mapper = mapper;
    }
    
    public async Task<ScheduleCellDTO> Handle(GetScheduleCellByIdQuery request, CancellationToken cancellationToken)
    {
        var scheduleCell = await scheduleCellRepository.GetByIdAsync(request.Id);

        if (scheduleCell is null)
        {
            throw new EntityNotFoundException(nameof(request.Id));
        }

        var scheduleCellModel = mapper.Map<ScheduleCellDTO>(scheduleCell);

        return scheduleCellModel;
    }
}
