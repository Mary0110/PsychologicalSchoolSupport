using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;
using PsychologicalSupportPlatform.Meet.Domain.Entities;

namespace PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Commands.Create;

public class CreateScheduleCellCommandHandler: IRequestHandler<CreateScheduleCellCommand, ResponseInfo>
{
    private readonly IScheduleCellRepository scheduleCellRepository;
    private readonly IMapper mapper;
    
    public CreateScheduleCellCommandHandler(IScheduleCellRepository scheduleCellRepository, IMapper mapper)
    {
        this.scheduleCellRepository = scheduleCellRepository;
        this.mapper = mapper;
    }

    public async Task<ResponseInfo> Handle(CreateScheduleCellCommand request, CancellationToken cancellationToken)
    {
        var newScheduleCell = mapper.Map<ScheduleCell>(request);
        
        if (newScheduleCell is null) throw new WrongRequestDataException();

        var oldScheduleCell = await scheduleCellRepository.GetScheduleCellsByDayAndTimeAsync(newScheduleCell.Day, newScheduleCell.Time);
        
        if (oldScheduleCell.Count != 0) throw new AlreadyExistsException();
       
        await scheduleCellRepository.AddScheduleCellsAsync(newScheduleCell);
        await scheduleCellRepository.SaveScheduleCellsAsync();

        return new ResponseInfo(success: true, message: "scheduleCell created");
    }
}
