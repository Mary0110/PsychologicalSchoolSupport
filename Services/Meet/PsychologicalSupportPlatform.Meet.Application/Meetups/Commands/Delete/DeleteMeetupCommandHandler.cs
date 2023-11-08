using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Delete;

public class DeleteMeetupCommandHandler: IRequestHandler<DeleteMeetupCommand, ResponseInfo>
{
    private readonly IMeetupRepository meetupRepository;
    private readonly IMapper mapper;
    
    public DeleteMeetupCommandHandler(IMeetupRepository meetupRepository, IMapper mapper)
    {
        this.meetupRepository = meetupRepository;
        this.mapper = mapper;
    }
    
    public async Task<ResponseInfo> Handle(DeleteMeetupCommand request, CancellationToken cancellationToken)
    {
        var opening = await meetupRepository.GetMeetingByIdAsync(request.Id);
        
        if (opening is null) return new ResponseInfo(success: false, message: $"wrong request data, no meetup with id {request.Id}");

        await meetupRepository.DeleteMeetingAsync(opening);
        
        return new ResponseInfo(success: true, message: "meetup deleted");
    }
}
