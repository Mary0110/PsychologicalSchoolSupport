using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Queries;

public class GetMeetupByIdQueryHandler: IRequestHandler<GetMeetupByIdQuery, DataResponseInfo<MeetupDTO>>
{
    private readonly IMeetupRepository meetupRepository;
    private readonly IMapper mapper;
    
    public GetMeetupByIdQueryHandler(IMeetupRepository meetupRepository, IMapper mapper)
    {
        this.meetupRepository = meetupRepository;
        this.mapper = mapper;
    }

    public async Task<DataResponseInfo<MeetupDTO>> Handle(GetMeetupByIdQuery request, CancellationToken cancellationToken)
    {
        var meetup = await meetupRepository.GetMeetingByIdAsync(request.Id);
        
        if (meetup is null) return new DataResponseInfo<MeetupDTO>(data: null, success: false, message: $"no meetup with id {request.Id}");

        var meetupModel = mapper.Map<MeetupDTO>(meetup);
        
        return new DataResponseInfo<MeetupDTO>(data: meetupModel, success: true, message: $"meetup with id {meetupModel.Id}");;   
    }
}
