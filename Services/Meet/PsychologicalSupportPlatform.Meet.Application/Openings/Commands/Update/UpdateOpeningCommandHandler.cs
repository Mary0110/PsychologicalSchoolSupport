using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Domain.Entities;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Openings.Commands.Update;

public class UpdateOpeningCommandHandler: IRequestHandler<UpdateOpeningCommand, ResponseInfo>
{
    private readonly IOpeningRepository openingRepository;
    private readonly IMapper mapper;
    
    public UpdateOpeningCommandHandler(IOpeningRepository openingRepository, IMapper mapper)
    {
        this.openingRepository = openingRepository;
        this.mapper = mapper;
    }

    public async Task<ResponseInfo> Handle(UpdateOpeningCommand request, CancellationToken cancellationToken)
    {
        var opening = mapper.Map<Opening>(request.openingDTO);
        
        if (opening is null) return new ResponseInfo(success: false, message: "wrong request data");
        
        var oldOpening = await openingRepository.GetOpeningByIdAsync(opening.Id);
        
        if (oldOpening is null) return new ResponseInfo(success: false, message: $"wrong request data, meetup with id {opening.Id} doesn't exist");

        await openingRepository.UpdateOpeningsAsync(opening);

        return new ResponseInfo(success: true, message: "opening updated");
    }
}
