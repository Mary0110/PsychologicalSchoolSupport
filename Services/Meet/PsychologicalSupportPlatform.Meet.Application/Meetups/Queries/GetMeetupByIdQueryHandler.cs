using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Domain.Entities;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Queries;

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
        var meetup = await meetupRepository.GetByIdAsync(request.Id);

        if (meetup is null)
        {
            throw new EntityNotFoundException(paramname: nameof(request.Id));
        }

        var meetupModel = mapper.Map<MeetupDTO>(meetup);
        
        return meetupModel;   
    }
}
