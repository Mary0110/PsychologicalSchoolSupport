using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;
using PsychologicalSupportPlatform.Meet.Domain.Entities;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Commands.Create;

public class CreateScheduleCellCommandHandler: IRequestHandler<CreateScheduleCellCommand, int>
{
    private readonly IScheduleCellRepository _scheduleCellRepository;
    private readonly IMapper _mapper;
    
    public CreateScheduleCellCommandHandler(IScheduleCellRepository scheduleCellRepository, IMapper mapper)
    {
        _scheduleCellRepository = scheduleCellRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateScheduleCellCommand request, CancellationToken cancellationToken)
    {
        var newScheduleCell = _mapper.Map<ScheduleCell>(request);

        var oldScheduleCells = await _scheduleCellRepository.GetScheduleCellsByDayAndTimeAsync(
            newScheduleCell.Day, newScheduleCell.Time, newScheduleCell.PsychologistId);

        if (oldScheduleCells.Count != 0)
        {
            throw new AlreadyExistsException();
        }
       
        var addedCell = await _scheduleCellRepository.AddAsync(newScheduleCell);
        await _scheduleCellRepository.SaveAsync();

        return addedCell.Id;
    }
}
