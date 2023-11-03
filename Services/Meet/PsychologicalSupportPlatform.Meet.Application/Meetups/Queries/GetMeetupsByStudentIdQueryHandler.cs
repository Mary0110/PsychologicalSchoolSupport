using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Application.Exceptions;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetup.Queries;

public class GetMeetupsByStudentIdQueryHandler: IRequestHandler<GetMeetupByStudentIdQuery, List<MeetupDTO>>
{
    private readonly IMeetupRepository meetupRepository;
    private readonly IMapper mapper;
    
    public GetMeetupsByStudentIdQueryHandler(IMeetupRepository meetupRepository, IMapper mapper)
    {
        this.meetupRepository = meetupRepository;
        this.mapper = mapper;
    }

    public async Task<List<MeetupDTO>> Handle(GetMeetupByStudentIdQuery request, CancellationToken cancellationToken)
    {
        var meetups = await meetupRepository.GetMeetingsByStudentIdAsync(request.StudentId);
        
        if (meetups == null)
        {
            throw new EntityNotFoundException("No such meetups");
        }
        
        var meetupModel = mapper.Map<List<MeetupDTO>>(meetups);
        
        return meetupModel;  
    }
}