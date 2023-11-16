using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Commands.Delete;

public class DeleteScheduleCellCommandHandler: IRequestHandler<DeleteScheduleCellCommand, ResponseInfo>
{
    private readonly IScheduleCellRepository scheduleCellRepository;
    private readonly IMapper mapper;
    
    public DeleteScheduleCellCommandHandler(IScheduleCellRepository scheduleCellRepository, IMapper mapper)
    {
        this.scheduleCellRepository = scheduleCellRepository;
        this.mapper = mapper;
    }
    
    public async Task<ResponseInfo> Handle(DeleteScheduleCellCommand request, CancellationToken cancellationToken)
    {
        var scheduleCell = await scheduleCellRepository.GetScheduleCellByIdAsync(request.Id);
        
        if (scheduleCell is null) throw new EntityNotFoundException(paramname: nameof(request.Id));

        await scheduleCellRepository.DeleteScheduleCellsAsync(scheduleCell);

        return new ResponseInfo(success: true, message: "scheduleCell deleted");
    }
}
