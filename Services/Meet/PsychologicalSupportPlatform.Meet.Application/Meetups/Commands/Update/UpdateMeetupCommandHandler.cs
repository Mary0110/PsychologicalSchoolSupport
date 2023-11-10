using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Domain.Entities;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Update;

public class UpdateMeetupCommandHandler: IRequestHandler<UpdateMeetupCommand, ResponseInfo>
{
    private readonly IMeetupRepository meetupRepository;
    private readonly IOpeningRepository openingRepository;
    private readonly IMapper mapper;
    
    public UpdateMeetupCommandHandler(IMeetupRepository meetupRepository, IMapper mapper,  IOpeningRepository openingRepository)
    {
        this.meetupRepository = meetupRepository;
        this.openingRepository = openingRepository;
        this.mapper = mapper;
    }

    public async Task<ResponseInfo> Handle(UpdateMeetupCommand request, CancellationToken cancellationToken)
    {
        var meetup = mapper.Map<Meetup>(request.MeetupDTO);
        
        if (meetup is null) return new ResponseInfo(success: false, message: "wrong request data");

        var oldMeetup = meetupRepository.GetMeetingByIdAsync(meetup.Id);
        
        if (oldMeetup is null) throw new EntityNotFoundException(paramname: nameof(meetup.Id));

        var newOpening = await openingRepository.GetOpeningByIdAsync(meetup.OpeningId);

        if (newOpening is null) throw new EntityNotFoundException(paramname: nameof(meetup.OpeningId));
        
        await meetupRepository.UpdateMeetingAsync(meetup);
        
        return new ResponseInfo(success: true, message: "meetup updated");
    }
}
