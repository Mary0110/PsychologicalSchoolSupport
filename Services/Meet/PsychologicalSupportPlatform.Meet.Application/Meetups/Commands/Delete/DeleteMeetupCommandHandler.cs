using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Meet.Application.Exceptions;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Delete;

public class DeleteMeetupCommandHandler: IRequestHandler<DeleteMeetupCommand>
{
    private readonly IMeetupRepository meetupRepository;
    private readonly IMapper mapper;
    
    public DeleteMeetupCommandHandler(IMeetupRepository meetupRepository, IMapper mapper)
    {
        this.meetupRepository = meetupRepository;
        this.mapper = mapper;
    }
    
    public async Task Handle(DeleteMeetupCommand request, CancellationToken cancellationToken)
    {
        var opening = await meetupRepository.GetMeetingByIdAsync(request.Id);
        
        if (opening == null)
        {
            throw new EntityNotFoundException("Meeting with this id does not exist");
        }
        
        await meetupRepository.DeleteMeetingAsync(opening);
    }
}