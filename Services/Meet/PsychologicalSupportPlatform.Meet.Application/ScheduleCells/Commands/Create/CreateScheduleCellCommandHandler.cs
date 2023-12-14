using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;
using PsychologicalSupportPlatform.Meet.Domain.Entities;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Commands.Create;

public class CreateScheduleCellCommandHandler: IRequestHandler<CreateScheduleCellCommand, int>
{
    private readonly IScheduleCellRepository scheduleCellRepository;
    private readonly IMapper mapper;
    
    public CreateScheduleCellCommandHandler(IScheduleCellRepository scheduleCellRepository, IMapper mapper)
    {
        this.scheduleCellRepository = scheduleCellRepository;
        this.mapper = mapper;
    }

    public async Task<int> Handle(CreateScheduleCellCommand request, CancellationToken cancellationToken)
    {
        var newScheduleCell = mapper.Map<ScheduleCell>(request);

        if (newScheduleCell is null)
        {
            throw new WrongRequestDataException();
        }

        var oldScheduleCells = await scheduleCellRepository.GetScheduleCellsByDayAndTimeAsync(
            newScheduleCell.Day, newScheduleCell.Time, newScheduleCell.PsychologistId);

        if (oldScheduleCells.Count != 0)
        {
            throw new AlreadyExistsException();
        }
       
        var addedCell = await scheduleCellRepository.AddAsync(newScheduleCell);
        await scheduleCellRepository.SaveAsync();

        return addedCell.Id;
    }
}
