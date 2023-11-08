using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;
using PsychologicalSupportPlatform.Meet.Domain.Entities;

namespace PsychologicalSupportPlatform.Meet.Application.Openings.Commands.Create;

public class CreateOpeningCommandHandler: IRequestHandler<CreateOpeningCommand, ResponseInfo>
{
    private readonly IOpeningRepository openingRepository;
    private readonly IMapper mapper;
    
    public CreateOpeningCommandHandler(IOpeningRepository openingRepository, IMapper mapper)
    {
        this.openingRepository = openingRepository;
        this.mapper = mapper;
    }

    public async Task<ResponseInfo> Handle(CreateOpeningCommand request, CancellationToken cancellationToken)
    {
        var newOpening = mapper.Map<Opening>(request);
        
        if (newOpening is null) return new ResponseInfo(success: false, message: "wrong request data");

        var oldOpening = await openingRepository.GetOpeningsByDayAndTimeAsync(newOpening.Day, newOpening.Time);
        
        if (oldOpening.Count != 0) return new ResponseInfo(success: false, message: "opening already exists");
       
        await openingRepository.AddOpeningsAsync(newOpening);
        await openingRepository.SaveOpeningsAsync();

        return new ResponseInfo(success: true, message: "opening created");
    }
}
