using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Application.DTOs.ScheduleCell;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Queries;

public class GetScheduleCellByIdHandler : IRequestHandler<GetScheduleCellByIdQuery, ScheduleCellDTO>
{
    private readonly IScheduleCellRepository _scheduleCellRepository;
    private readonly IMapper _mapper;

    public GetScheduleCellByIdHandler(IScheduleCellRepository scheduleCellRepository, IMapper mapper)
    {
        _scheduleCellRepository = scheduleCellRepository;
        _mapper = mapper;
    }
    
    public async Task<ScheduleCellDTO> Handle(GetScheduleCellByIdQuery request, CancellationToken cancellationToken)
    {
        var scheduleCell = await _scheduleCellRepository.GetByIdAsync(request.Id);

        if (scheduleCell is null)
        {
            throw new EntityNotFoundException(nameof(request.Id));
        }

        var scheduleCellModel = _mapper.Map<ScheduleCellDTO>(scheduleCell);

        return scheduleCellModel;
    }
}
