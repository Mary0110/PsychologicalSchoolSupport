using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Application.DTOs.Meetup;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Queries;

public class GetMeetupsByStudentIdQueryHandler: IRequestHandler<GetMeetupsByStudentIdQuery, List<MeetupDTO>>
{
    private readonly IMeetupRepository _meetupRepository;
    private readonly IMapper _mapper;
    
    public GetMeetupsByStudentIdQueryHandler(IMeetupRepository meetupRepository, IMapper mapper)
    {
        _meetupRepository = meetupRepository;
        _mapper = mapper;
    }

    public async Task<List<MeetupDTO>> Handle(GetMeetupsByStudentIdQuery request, CancellationToken cancellationToken)
    {
        var meetups = await _meetupRepository.GetAllAsync(
            m => m.StudentId == request.StudentId, request.PageNumber, request.PageSize
            );

        if (meetups is null)
        {
            throw new EntityNotFoundException(paramname: nameof(request.StudentId));
        }

        var meetupModels = _mapper.Map<List<MeetupDTO>>(meetups);
        
        return meetupModels;    
    }
}
