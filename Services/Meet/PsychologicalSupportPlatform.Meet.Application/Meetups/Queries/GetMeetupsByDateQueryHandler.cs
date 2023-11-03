using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Application.Exceptions;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetup.Queries;

public class GetMeetupsByDateQueryHandler: IRequestHandler<GetMeetupsByDateQuery, List<MeetupDTO>>
{
    private readonly IMeetupRepository meetupRepository;
    private readonly IMapper mapper;
    
    public GetMeetupsByDateQueryHandler(IMeetupRepository meetupRepository, IMapper mapper)
    {
        this.meetupRepository = meetupRepository;
        this.mapper = mapper;
    }

    public async Task<List<MeetupDTO>> Handle(GetMeetupsByDateQuery request, CancellationToken cancellationToken)
    {
        var meetup = await meetupRepository.GetMeetingsByDateAsync(request.Date);
        
        if (meetup == null)
        {
            throw new EntityNotFoundException("No such meetups");
        }
        
        var meetupModel = mapper.Map<List<MeetupDTO>>(meetup);
        
        return meetupModel;    
    }
}