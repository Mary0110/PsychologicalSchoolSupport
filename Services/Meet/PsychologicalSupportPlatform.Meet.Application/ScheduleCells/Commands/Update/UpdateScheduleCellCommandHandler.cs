using Mapster;
using MediatR;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Commands.Update;

public class UpdateScheduleCellCommandHandler: IRequestHandler<UpdateScheduleCellCommand, int>
{
    private readonly IScheduleCellRepository _scheduleCellRepository;
    
    public UpdateScheduleCellCommandHandler(IScheduleCellRepository scheduleCellRepository)
    {
        _scheduleCellRepository = scheduleCellRepository;
    }

    public async Task<int> Handle(UpdateScheduleCellCommand request, CancellationToken cancellationToken)
    {
        var oldScheduleCell = await _scheduleCellRepository.GetAsync(cell => cell.Id == request.Id);

        if (oldScheduleCell is null)
        {
            throw new EntityNotFoundException(paramname: nameof(request.Id));
        }

        var cellToUpdate = request.Adapt(oldScheduleCell);
        
        await _scheduleCellRepository.UpdateAsync(cellToUpdate);
        await _scheduleCellRepository.SaveAsync();

        return cellToUpdate.Id;
    }
}
