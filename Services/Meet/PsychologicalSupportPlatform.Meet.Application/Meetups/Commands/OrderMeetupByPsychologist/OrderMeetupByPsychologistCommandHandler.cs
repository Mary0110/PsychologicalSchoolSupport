using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Domain.Entities;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.OrderMeetupByPsychologist;

public class OrderMeetupByPsychologistCommandHandler: IRequestHandler<OrderMeetupByPsychologistCommand, int>
{
    private readonly IMeetupRepository _meetupRepository;
    private readonly IScheduleCellRepository _scheduleCellRepository;
    private readonly IMapper _mapper;
    
    public OrderMeetupByPsychologistCommandHandler(IMeetupRepository meetupRepository, 
        IScheduleCellRepository scheduleCellRepository, IMapper mapper)
    {
        _meetupRepository = meetupRepository;
        _scheduleCellRepository = scheduleCellRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(OrderMeetupByPsychologistCommand request, CancellationToken cancellationToken)
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

        var creatorId = int.Parse(request.PsyId);

        if (creatorId != chosenScheduleCell.PsychologistId)
        {
            throw new NotAllowedActionForTheUserException(creatorId);
        }

        var newMeetup = _mapper.Map<Meetup>(request);

        var addedMeetup = await _meetupRepository.AddAsync(newMeetup);
        await _meetupRepository.SaveAsync();

        return addedMeetup.Id;
    }
}
