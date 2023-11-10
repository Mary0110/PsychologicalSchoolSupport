using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Domain.Entities;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.OrderMeetup;

public class OrderMeetupCommandHandler: IRequestHandler<OrderMeetupCommand, ResponseInfo>
{
    private readonly IMeetupRepository meetupRepository;
    private readonly IOpeningRepository openingRepository;
    private readonly IMapper mapper;
    
    public OrderMeetupCommandHandler(IMeetupRepository meetupRepository, IOpeningRepository openingRepository, IMapper mapper)
    {
        this.meetupRepository = meetupRepository;
        this.openingRepository = openingRepository;
        this.mapper = mapper;
    }

    public async Task<ResponseInfo> Handle(OrderMeetupCommand request, CancellationToken cancellationToken)
    {
        var chosenOpening = await openingRepository.GetOpeningByIdAsync(request.MeetupDto.OpeningId);
        
        if (chosenOpening is null) throw new EntityNotFoundException(paramname: nameof(request.MeetupDto.OpeningId));

        if (request.MeetupDto.Date.DayOfWeek != chosenOpening.Day)
            return new ResponseInfo(success: false, message: "opening is from different weekday");

        if (!IsOpeningAvailable(chosenOpening))
            return new ResponseInfo(success: false, message: "the opening is already reserved");

        var newMeetup = mapper.Map<Meetup>(request);
        
        if (newMeetup is null) return new ResponseInfo(success: false, message: "wrong request data");
        
        await meetupRepository.AddMeetingAsync(newMeetup);
        await meetupRepository.SaveMeetingAsync();     
        
        return new ResponseInfo(success: true, message: "meetup created");
    }
    
    private bool IsOpeningAvailable(Opening opening)
    {
        var hasOrderedMeetups = opening.Meetups.Any(m => m.Date > DateOnly.FromDateTime(DateTime.Now));
        
        if (!hasOrderedMeetups)
        {
            var hasOrderedMeetupsToday = opening.Meetups.Any(m => m.Date == DateOnly.FromDateTime(DateTime.Now));
            
            if (hasOrderedMeetupsToday)
            {
                hasOrderedMeetups = opening.Time >= TimeOnly.FromDateTime(DateTime.Now);
            }
        }

        return opening.Active && !hasOrderedMeetups;
    }
}
