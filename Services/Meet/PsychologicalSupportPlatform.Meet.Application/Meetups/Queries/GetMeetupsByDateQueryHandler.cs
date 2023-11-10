using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Queries;

public class GetMeetupsByDateQueryHandler: IRequestHandler<GetMeetupsByDateQuery, DataResponseInfo<List<MeetupDTO>>>
{
    private readonly IMeetupRepository meetupRepository;
    private readonly IMapper mapper;
    
    public GetMeetupsByDateQueryHandler(IMeetupRepository meetupRepository, IMapper mapper)
    {
        this.meetupRepository = meetupRepository;
        this.mapper = mapper;
    }

    public async Task<DataResponseInfo<List<MeetupDTO>>> Handle(GetMeetupsByDateQuery request, CancellationToken cancellationToken)
    {
        var meetup = await meetupRepository.GetMeetingsByDateAsync(request.Date);
        
        if (meetup is null) throw new EntityNotFoundException(paramname: nameof(request.Date));

        var meetupModel = mapper.Map<List<MeetupDTO>>(meetup);
        
        return new DataResponseInfo<List<MeetupDTO>>(data: meetupModel, success: true, message: $"meetups with date {request.Date}");    
    }
}
