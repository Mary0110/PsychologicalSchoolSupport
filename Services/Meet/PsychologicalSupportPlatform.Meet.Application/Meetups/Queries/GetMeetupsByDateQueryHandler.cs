using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Domain.Entities;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Queries;

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
        var meetup = await meetupRepository.GetAllAsync(
            m => m.Date == request.Date, request.pageNumber, request.pageSize
            );

        if (meetup is null)
        {
            throw new EntityNotFoundException(paramname: nameof(request.Date));
        }

        var meetupModel = mapper.Map<List<MeetupDTO>>(meetup);
        
        return meetupModel;    
    }
}
