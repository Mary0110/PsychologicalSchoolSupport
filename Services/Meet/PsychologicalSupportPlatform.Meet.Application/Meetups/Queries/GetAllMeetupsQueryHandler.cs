using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Application.Exceptions;
using PsychologicalSupportPlatform.Meet.Application.Meetups.Queries;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetup.Queries;

public class GetAllMeetupsQueryHandler : IRequestHandler<GetAllMeetupsQuery, List<MeetupDTO>>
{
    private readonly IMeetupRepository meetupRepository;
    private readonly IMapper mapper;
    
    public GetAllMeetupsQueryHandler(IMeetupRepository meetupRepository, IMapper mapper)
    {
        this.meetupRepository = meetupRepository;
        this.mapper = mapper;
    }

    public async Task<List<MeetupDTO>> Handle(GetAllMeetupsQuery request, CancellationToken cancellationToken)
    {
        var meetups = await meetupRepository.GetAllMeetingsAsync();
        
        if (meetups == null)
        {
            throw new EntityNotFoundException("No meetups");
        }
        
        var meetupsModel = mapper.Map<List<MeetupDTO>>(meetups);
        
        return meetupsModel;
    }
}