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
        if (request.scheduleCellDTO is null)
        {
            throw new WrongRequestDataException();
        }
        
        var oldScheduleCell = await _scheduleCellRepository.GetAsync(cell => cell.Id == request.id);

        if (oldScheduleCell is null)
        {
            throw new EntityNotFoundException(paramname: nameof(request.id));
        }

        var cellToUpdate = request.scheduleCellDTO.Adapt(oldScheduleCell);
        
        await _scheduleCellRepository.UpdateAsync(cellToUpdate);
        await _scheduleCellRepository.SaveAsync();

        return cellToUpdate.Id;
    }
}
