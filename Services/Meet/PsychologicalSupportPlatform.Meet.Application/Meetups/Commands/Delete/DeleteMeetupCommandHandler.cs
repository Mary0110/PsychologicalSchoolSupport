using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Delete;

public class DeleteMeetupCommandHandler: IRequestHandler<DeleteMeetupCommand, int>
{
    private readonly IMeetupRepository meetupRepository;
    private readonly IMapper mapper;
    
    public DeleteMeetupCommandHandler(IMeetupRepository meetupRepository, IMapper mapper)
    {
        this.meetupRepository = meetupRepository;
        this.mapper = mapper;
    }
    
    public async Task<int> Handle(DeleteMeetupCommand request, CancellationToken cancellationToken)
    {
        var meetup = await meetupRepository.GetByIdAsync(request.Id);

        if (meetup is null)
        {
            throw new EntityNotFoundException(paramname: nameof(request.Id));
        }
        
        meetupRepository.Delete(meetup);
        await meetupRepository.SaveAsync();

        return meetup.Id;
    }
}
