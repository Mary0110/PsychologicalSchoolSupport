using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Delete;

public class DeleteMeetupCommandHandler: IRequestHandler<DeleteMeetupCommand, int>
{
    private readonly IMeetupRepository meetupRepository;
    
    public DeleteMeetupCommandHandler(IMeetupRepository meetupRepository)
    {
        this.meetupRepository = meetupRepository;
    }
    
    public async Task<int> Handle(DeleteMeetupCommand request, CancellationToken cancellationToken)
    {
        var meetup = await meetupRepository.GetAsync(m => m.Id == request.Id);

        if (meetup is null)
        {
            throw new EntityNotFoundException(paramname: nameof(request.Id));
        }
        
        await meetupRepository.DeleteAsync(meetup);
        await meetupRepository.SaveAsync();

        return meetup.Id;
    }
}
