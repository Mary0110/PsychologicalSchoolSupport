using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Commands.Delete;

public class DeleteScheduleCellCommandHandler: IRequestHandler<DeleteScheduleCellCommand, int>
{
    private readonly IScheduleCellRepository _scheduleCellRepository;
    
    public DeleteScheduleCellCommandHandler(IScheduleCellRepository scheduleCellRepository)
    {
        _scheduleCellRepository = scheduleCellRepository;
    }
    
    public async Task<int> Handle(DeleteScheduleCellCommand request, CancellationToken cancellationToken)
    {
        var scheduleCell = await _scheduleCellRepository.GetAsync(cell => cell.Id == request.Id);

        if (scheduleCell is null)
        {
            throw new EntityNotFoundException(paramname: nameof(request.Id));
        }

        await _scheduleCellRepository.DeleteAsync(scheduleCell);
        await _scheduleCellRepository.SaveAsync();
        
        return scheduleCell.Id;
    }
}
