using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Domain.Entities;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.OrderMeetup;

public class OrderMeetupCommandHandler: IRequestHandler<OrderMeetupCommand, int>
{
    private readonly IMeetupRepository meetupRepository;
    private readonly IScheduleCellRepository scheduleCellRepository;
    private readonly IMapper mapper;
    
    public OrderMeetupCommandHandler(IMeetupRepository meetupRepository, IScheduleCellRepository scheduleCellRepository, IMapper mapper)
    {
        this.meetupRepository = meetupRepository;
        this.scheduleCellRepository = scheduleCellRepository;
        this.mapper = mapper;
    }

    public async Task<int> Handle(OrderMeetupCommand request, CancellationToken cancellationToken)
    {
        var chosenScheduleCell = await scheduleCellRepository.GetByIdAsync(request.MeetupDto.ScheduleCellId);

        if (chosenScheduleCell is null)
        {
            throw new EntityNotFoundException(paramname: nameof(request.MeetupDto.ScheduleCellId));
        }

        if (request.MeetupDto.Date.DayOfWeek != chosenScheduleCell.Day)
        {
            throw new WrongRequestDataException();
        }

        if (!IsScheduleCellAvailable(chosenScheduleCell))
        {
            throw new AlreadyExistsException();
        }

        var newMeetup = mapper.Map<Meetup>(request);

        if (newMeetup is null)
        {
            throw new WrongRequestDataException();
        }
        
        var addedMeetup = await meetupRepository.AddAsync(newMeetup);
        await meetupRepository.SaveAsync();

        return addedMeetup.Id;
    }
    
    private bool IsScheduleCellAvailable(ScheduleCell scheduleCell)
    {
        var hasOrderedMeetups = scheduleCell.Meetups.Any(m => m.Date > DateOnly.FromDateTime(DateTime.Now));
        
        if (!hasOrderedMeetups)
        {
            var hasOrderedMeetupsToday = scheduleCell.Meetups.Any(m => m.Date == DateOnly.FromDateTime(DateTime.Now));
            
            if (hasOrderedMeetupsToday)
            {
                hasOrderedMeetups = scheduleCell.Time >= TimeOnly.FromDateTime(DateTime.Now);
            }
        }

        return scheduleCell.Active && !hasOrderedMeetups;
    }
}
