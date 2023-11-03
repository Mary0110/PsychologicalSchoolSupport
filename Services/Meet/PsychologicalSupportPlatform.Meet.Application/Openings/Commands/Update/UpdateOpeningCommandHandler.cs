using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Meet.Application.Exceptions;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Openings.Commands.Update;

public class UpdateOpeningCommandHandler: IRequestHandler<UpdateOpeningCommand>
{
    private readonly IOpeningRepository openingRepository;
    private readonly IMapper mapper;
    
    public UpdateOpeningCommandHandler(IOpeningRepository openingRepository, IMapper mapper)
    {
        this.openingRepository = openingRepository;
        this.mapper = mapper;
    }

    public async Task Handle(UpdateOpeningCommand request, CancellationToken cancellationToken)
    {
        var opening = mapper.Map<Domain.Entities.Opening>(request.openingDTO);
        
        if (opening == null)
        {
            throw new EntityNotFoundException("Event with this id does not exist");
        }

        await openingRepository.UpdateOpeningsAsync(opening);
    }
}