using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Domain.Entities;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Commands.Update;

public class UpdateScheduleCellCommandHandler: IRequestHandler<UpdateScheduleCellCommand, int>
{
    private readonly IScheduleCellRepository scheduleCellRepository;
    private readonly IMapper mapper;
    
    public UpdateScheduleCellCommandHandler(IScheduleCellRepository scheduleCellRepository, IMapper mapper)
    {
        this.scheduleCellRepository = scheduleCellRepository;
        this.mapper = mapper;
    }

    public async Task<int> Handle(UpdateScheduleCellCommand request, CancellationToken cancellationToken)
    {
        var scheduleCell = mapper.Map<ScheduleCell>(request.scheduleCellDTO);

        if (scheduleCell is null)
        {
            throw new WrongRequestDataException();
        }
        
        var oldScheduleCell = await scheduleCellRepository.GetAsync(cell => cell.Id == scheduleCell.Id);

        if (oldScheduleCell is null)
        {
            throw new EntityNotFoundException(paramname: nameof(scheduleCell.Id));
        }
        
        await scheduleCellRepository.UpdateAsync(scheduleCell);
        await scheduleCellRepository.SaveAsync();

        return scheduleCell.Id;
    }
}
