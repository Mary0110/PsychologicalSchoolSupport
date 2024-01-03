using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Application.DTOs.Meetup;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Queries;

public class GetMeetupsByDateQueryHandler: IRequestHandler<GetMeetupsByDateQuery, List<MeetupDTO>>
{
    private readonly IMeetupRepository _meetupRepository;
    private readonly IMapper _mapper;
    
    public GetMeetupsByDateQueryHandler(IMeetupRepository meetupRepository, IMapper mapper)
    {
        _meetupRepository = meetupRepository;
        _mapper = mapper;
    }

    public async Task<List<MeetupDTO>> Handle(GetMeetupsByDateQuery request, CancellationToken cancellationToken)
    {
        var meetup = await _meetupRepository.GetAllAsync(
            m => m.Date == request.Date, request.PageNumber, request.PageSize
            );

        if (meetup is null)
        {
            throw new EntityNotFoundException(paramname: nameof(request.Date));
        }

        var meetupModel = _mapper.Map<List<MeetupDTO>>(meetup);
        
        return meetupModel;    
    }
}
