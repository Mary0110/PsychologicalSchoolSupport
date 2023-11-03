using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Meet.Application.Exceptions;
using PsychologicalSupportPlatform.Meet.Application.Openings.Commands.Update;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Update;

public class UpdateMeetupCommandHandler: IRequestHandler<UpdateMeetupCommand>
{
    private readonly IMeetupRepository meetupRepository;
    private readonly IMapper mapper;
    
    public UpdateMeetupCommandHandler(IMeetupRepository meetupRepository, IMapper mapper)
    {
        this.meetupRepository = meetupRepository;
        this.mapper = mapper;
    }

    public async Task Handle(UpdateMeetupCommand request, CancellationToken cancellationToken)
    {
        var meetup = mapper.Map<Domain.Entities.Meetup>(request.MeetupDTO);
        
        if (meetup == null)
        {
            throw new EntityNotFoundException("Meetup with this id does not exist");
        }

        await meetupRepository.UpdateMeetingAsync(meetup);
    }
}