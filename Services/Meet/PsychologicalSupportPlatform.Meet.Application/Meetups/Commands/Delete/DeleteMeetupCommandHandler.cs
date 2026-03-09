using MediatR;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Delete;

public class DeleteMeetupCommandHandler: IRequestHandler<DeleteMeetupCommand, int>
{
    private readonly IMeetupRepository _meetupRepository;
    
    public DeleteMeetupCommandHandler(IMeetupRepository meetupRepository)
    {
        _meetupRepository = meetupRepository;
    }
    
    public async Task<int> Handle(DeleteMeetupCommand request, CancellationToken cancellationToken)
    {
        var meetup = await _meetupRepository.GetAsync(m => m.Id == request.Id);

        if (meetup is null)
        {
            throw new EntityNotFoundException(paramname: nameof(request.Id));
        }
        
        await _meetupRepository.DeleteAsync(meetup);
        await _meetupRepository.SaveAsync();

        return meetup.Id;
    }
}
