using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Meet.Application.Exceptions;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Openings.Commands.Delete;

public class DeleteOpeningCommandHandler: IRequestHandler<DeleteOpeningCommand>
{
    private readonly IOpeningRepository openingRepository;
    private readonly IMapper mapper;
    
    public DeleteOpeningCommandHandler(IOpeningRepository openingRepository, IMapper mapper)
    {
        this.openingRepository = openingRepository;
        this.mapper = mapper;
    }
    
    public async Task Handle(DeleteOpeningCommand request, CancellationToken cancellationToken)
    {
        var opening = await openingRepository.GetOpeningByIdAsync(request.Id);
        
        if (opening == null)
        {
            throw new EntityNotFoundException("Opening with this id does not exist");
        }
        
        await openingRepository.DeleteOpeningsAsync(opening);
    }
}