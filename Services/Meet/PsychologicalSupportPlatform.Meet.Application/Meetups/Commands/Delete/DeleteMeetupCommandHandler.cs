using MediatR;
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
        var deletedMeetup = await _meetupRepository.DeleteByIdAsync(request.Id);
        await _meetupRepository.SaveAsync();

        return deletedMeetup.Id;
    }
}
