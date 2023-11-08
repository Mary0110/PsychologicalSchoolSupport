using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Queries;

public class GetAllMeetupsQueryHandler : IRequestHandler<GetAllMeetupsQuery, DataResponseInfo<List<MeetupDTO>>>
{
    private readonly IMeetupRepository meetupRepository;
    private readonly IMapper mapper;
    
    public GetAllMeetupsQueryHandler(IMeetupRepository meetupRepository, IMapper mapper)
    {
        this.meetupRepository = meetupRepository;
        this.mapper = mapper;
    }

    public async Task<DataResponseInfo<List<MeetupDTO>>> Handle(GetAllMeetupsQuery request, CancellationToken cancellationToken)
    {
        var meetups = await meetupRepository.GetAllMeetingsAsync();
        
        if (meetups is null) return new DataResponseInfo<List<MeetupDTO>>(data: null, success: false, message: "no meetups");

        var meetupsModel = mapper.Map<List<MeetupDTO>>(meetups);
        
        return new DataResponseInfo<List<MeetupDTO>>(data: meetupsModel, success: true, message: "all meetups");
    }
}
