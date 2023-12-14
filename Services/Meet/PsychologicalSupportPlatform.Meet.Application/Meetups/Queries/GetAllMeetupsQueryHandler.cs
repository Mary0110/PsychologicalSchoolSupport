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

public class GetAllMeetupsQueryHandler : IRequestHandler<GetAllMeetupsQuery, List<MeetupDTO>>
{
    private readonly IMeetupRepository meetupRepository;
    private readonly IMapper mapper;
    
    public GetAllMeetupsQueryHandler(IMeetupRepository meetupRepository, IMapper mapper)
    {
        this.meetupRepository = meetupRepository;
        this.mapper = mapper;
    }

    public async Task<List<MeetupDTO>> Handle(GetAllMeetupsQuery request, CancellationToken cancellationToken)
    {
        var meetups = await meetupRepository.GetAllAsync(request.pageNumber, request.pageSize);

        if (meetups is null)
        {
            throw new EntityNotFoundException();
        }
        
        var meetupsModel = mapper.Map<List<MeetupDTO>>(meetups);
        
        return meetupsModel;
    }
}
