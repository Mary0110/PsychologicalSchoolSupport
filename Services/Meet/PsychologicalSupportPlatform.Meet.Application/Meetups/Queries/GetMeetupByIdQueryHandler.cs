using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Application.Exceptions;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetup.Queries;

public class GetMeetupByIdQueryHandler: IRequestHandler<GetMeetupByIdQuery, MeetupDTO>
{
    private readonly IMeetupRepository meetupRepository;
    private readonly IMapper mapper;
    
    public GetMeetupByIdQueryHandler(IMeetupRepository meetupRepository, IMapper mapper)
    {
        this.meetupRepository = meetupRepository;
        this.mapper = mapper;
    }

    public async Task<MeetupDTO> Handle(GetMeetupByIdQuery request, CancellationToken cancellationToken)
    {
        var meetup = await meetupRepository.GetMeetingByIdAsync(request.Id);
        
        if (meetup == null)
        {
            throw new EntityNotFoundException("No such opening");
        }
        
        var meetupModel = mapper.Map<MeetupDTO>(meetup);
        
        return meetupModel;   
    }
}