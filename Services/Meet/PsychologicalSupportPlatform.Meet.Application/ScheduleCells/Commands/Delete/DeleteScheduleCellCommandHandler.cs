using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Commands.Delete;

public class DeleteScheduleCellCommandHandler: IRequestHandler<DeleteScheduleCellCommand, int>
{
    private readonly IScheduleCellRepository scheduleCellRepository;
    private readonly IMapper mapper;
    
    public DeleteScheduleCellCommandHandler(IScheduleCellRepository scheduleCellRepository, IMapper mapper)
    {
        this.scheduleCellRepository = scheduleCellRepository;
        this.mapper = mapper;
    }
    
    public async Task<int> Handle(DeleteScheduleCellCommand request, CancellationToken cancellationToken)
    {
        var scheduleCell = await scheduleCellRepository.GetByIdAsync(request.Id);

        if (scheduleCell is null)
        {
            throw new EntityNotFoundException(paramname: nameof(request.Id));
        }

        scheduleCellRepository.Delete(scheduleCell);
        await scheduleCellRepository.SaveAsync();
        
        return scheduleCell.Id;
    }
}
