using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Application.DTOs.Meetup;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Queries;

public class GetAllMeetupsQueryHandler : IRequestHandler<GetAllMeetupsQuery, List<MeetupDTO>>
{
    private readonly IMeetupRepository _meetupRepository;
    private readonly IMapper _mapper;
    
    public GetAllMeetupsQueryHandler(IMeetupRepository meetupRepository, IMapper mapper)
    {
        _meetupRepository = meetupRepository;
        _mapper = mapper;
    }

    public async Task<List<MeetupDTO>> Handle(GetAllMeetupsQuery request, CancellationToken cancellationToken)
    {
        var meetups = await _meetupRepository.GetAllAsync(request.PageNumber, request.PageSize);

        if (meetups is null)
        {
            throw new EntityNotFoundException();
        }
        
        var meetupsModel = _mapper.Map<List<MeetupDTO>>(meetups);
        
        return meetupsModel;
    }
}
