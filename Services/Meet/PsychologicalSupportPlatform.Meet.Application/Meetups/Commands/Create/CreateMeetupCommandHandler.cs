using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;
using PsychologicalSupportPlatform.Meet.Domain.Entities;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Create;

public class CreateMeetupCommandHandler: IRequestHandler<CreateMeetupCommand, ResponseInfo>
{
    private readonly IMeetupRepository meetupRepository;
    private readonly IMapper mapper;
    
    public CreateMeetupCommandHandler(IMeetupRepository meetupRepository, IMapper mapper)
    {
        this.meetupRepository = meetupRepository;
        this.mapper = mapper;
    }

    public async Task<ResponseInfo> Handle(CreateMeetupCommand request, CancellationToken cancellationToken)
    {
        var newMeetup = mapper.Map<Meetup>(request);
        
        if (newMeetup is null) return new ResponseInfo(success: false, message: "wrong request data");

        await meetupRepository.AddMeetingAsync(newMeetup);
        await meetupRepository.SaveMeetingAsync();
        
        return new ResponseInfo(success: true, message: "meetup created");    
    }
}
