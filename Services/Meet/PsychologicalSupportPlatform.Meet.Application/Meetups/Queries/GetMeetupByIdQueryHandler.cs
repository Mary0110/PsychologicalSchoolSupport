using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Application.DTOs.Meetup;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Queries;

public class GetMeetupByIdQueryHandler: IRequestHandler<GetMeetupByIdQuery, MeetupDTO>
{
    private readonly IMeetupRepository _meetupRepository;
    private readonly IMapper _mapper;
    
    public GetMeetupByIdQueryHandler(IMeetupRepository meetupRepository, IMapper mapper)
    {
        _meetupRepository = meetupRepository;
        _mapper = mapper;
    }

    public async Task<MeetupDTO> Handle(GetMeetupByIdQuery request, CancellationToken cancellationToken)
    {
        var meetup = await _meetupRepository.GetByIdAsync(request.Id);

        if (meetup is null)
        {
            throw new EntityNotFoundException(paramname: nameof(request.Id));
        }

        var meetupModel = _mapper.Map<MeetupDTO>(meetup);
        
        return meetupModel;   
    }
}
