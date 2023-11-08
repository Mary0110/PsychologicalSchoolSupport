using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Queries;

public class GetMeetupsByStudentIdQueryHandler: IRequestHandler<GetMeetupsByStudentIdQuery, DataResponseInfo<List<MeetupDTO>>>
{
    private readonly IMeetupRepository meetupRepository;
    private readonly IMapper mapper;
    
    public GetMeetupsByStudentIdQueryHandler(IMeetupRepository meetupRepository, IMapper mapper)
    {
        this.meetupRepository = meetupRepository;
        this.mapper = mapper;
    }

    public async Task<DataResponseInfo<List<MeetupDTO>>> Handle(GetMeetupsByStudentIdQuery request, CancellationToken cancellationToken)
    {
        var meetups = await meetupRepository.GetMeetingsByStudentIdAsync(request.StudentId);
        
        if (meetups is null) return new DataResponseInfo<List<MeetupDTO>>(data: null, success: false, message: $"no meetups for student with id {request.StudentId}");

        var meetupModel = mapper.Map<List<MeetupDTO>>(meetups);
        
        return new DataResponseInfo<List<MeetupDTO>>(data: meetupModel, success: true, message: $"meetups for student with id {request.StudentId}");    
    }
}
