using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Create;

public class CreateMeetupCommandHandler: IRequestHandler<CreateMeetupCommand, int>
{
    private readonly IMeetupRepository meetupRepository;
    private readonly IMapper mapper;
    
    public CreateMeetupCommandHandler(IMeetupRepository meetupRepository, IMapper mapper)
    {
        this.meetupRepository = meetupRepository;
        this.mapper = mapper;
    }

    public async Task<int> Handle(CreateMeetupCommand request, CancellationToken cancellationToken)
    {
        var newMeetup = mapper.Map<Domain.Entities.Meetup>(request);
        Console.WriteLine($"handler{newMeetup.OpeningId}");
        await meetupRepository.AddMeetingAsync(newMeetup);
        await meetupRepository.SaveMeetingAsync();
        
        return newMeetup.Id;    
    }
}