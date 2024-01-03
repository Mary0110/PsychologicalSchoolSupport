using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Domain.Entities;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.OrderMeetup;

public class OrderMeetupCommandHandler: IRequestHandler<OrderMeetupCommand, int>
{
    private readonly IMeetupRepository _meetupRepository;
    private readonly IScheduleCellRepository _scheduleCellRepository;
    private readonly IMapper _mapper;
    
    public OrderMeetupCommandHandler(IMeetupRepository meetupRepository, 
        IScheduleCellRepository scheduleCellRepository, IMapper mapper)
    {
        _meetupRepository = meetupRepository;
        _scheduleCellRepository = scheduleCellRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(OrderMeetupCommand request, CancellationToken cancellationToken)
    {
        var chosenScheduleCell = await _scheduleCellRepository.GetByIdAsync(request.MeetupDto.ScheduleCellId);

        if (chosenScheduleCell is null)
        {
            throw new EntityNotFoundException(paramname: nameof(request.MeetupDto.ScheduleCellId));
        }

        if (request.MeetupDto.Date.DayOfWeek != chosenScheduleCell.Day)
        {
            throw new WrongDateWeekdayException();
        }

        if (!HandlerHelper.IsScheduleCellAvailable(chosenScheduleCell))
        {
            throw new AlreadyExistsException();
        }

        var newMeetup = _mapper.Map<Meetup>(request);

        var addedMeetup = await _meetupRepository.AddAsync(newMeetup);
        await _meetupRepository.SaveAsync();

        return addedMeetup.Id;
    }
}
