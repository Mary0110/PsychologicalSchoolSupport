using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;
using PsychologicalSupportPlatform.Meet.Domain.Entities;

namespace PsychologicalSupportPlatform.Meet.Application.Openings.Commands.Create;

public class CreateOpeningCommandHandler: IRequestHandler<CreateOpeningCommand, int>
{
    private readonly IOpeningRepository openingRepository;
    private readonly IMapper mapper;
    
    public CreateOpeningCommandHandler(IOpeningRepository openingRepository, IMapper mapper)
    {
        this.openingRepository = openingRepository;
        this.mapper = mapper;
    }

    public async Task<int> Handle(CreateOpeningCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"handler2{request.OpeningDto}");

        var newOpening = mapper.Map<Domain.Entities.Opening>(request);
        Console.WriteLine($"handler2{newOpening.Id}");

        await openingRepository.AddOpeningsAsync(newOpening);
        await openingRepository.SaveOpeningsAsync();
        
        return newOpening.Id; 
    }
}