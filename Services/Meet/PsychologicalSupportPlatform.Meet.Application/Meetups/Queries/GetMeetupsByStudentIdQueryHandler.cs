using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Common.Repository;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Application.DTOs.Meetup;
using PsychologicalSupportPlatform.Meet.Domain.Entities;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Queries;

public class GetMeetupsByStudentIdQueryHandler: IRequestHandler<GetMeetupsByStudentIdQuery, List<MeetupDTO>>
{
    private readonly IMeetupRepository meetupRepository;
    private readonly IMapper mapper;
    
    public GetMeetupsByStudentIdQueryHandler(IMeetupRepository meetupRepository, IMapper mapper)
    {
        this.meetupRepository = meetupRepository;
        this.mapper = mapper;
    }

    public async Task<List<MeetupDTO>> Handle(GetMeetupsByStudentIdQuery request, CancellationToken cancellationToken)
    {
        var meetups = await meetupRepository.GetAllAsync(
            m => m.StudentId == request.StudentId, request.pageNumber, request.pageSize
            );

        if (meetups is null)
        {
            throw new EntityNotFoundException(paramname: nameof(request.StudentId));
        }

        var meetupModels = mapper.Map<List<MeetupDTO>>(meetups);
        
        return meetupModels;    
    }
}
