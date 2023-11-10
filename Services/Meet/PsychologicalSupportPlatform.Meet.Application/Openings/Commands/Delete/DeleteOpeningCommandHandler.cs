using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Openings.Commands.Delete;

public class DeleteOpeningCommandHandler: IRequestHandler<DeleteOpeningCommand, ResponseInfo>
{
    private readonly IOpeningRepository openingRepository;
    private readonly IMapper mapper;
    
    public DeleteOpeningCommandHandler(IOpeningRepository openingRepository, IMapper mapper)
    {
        this.openingRepository = openingRepository;
        this.mapper = mapper;
    }
    
    public async Task<ResponseInfo> Handle(DeleteOpeningCommand request, CancellationToken cancellationToken)
    {
        var opening = await openingRepository.GetOpeningByIdAsync(request.Id);
        
        if (opening is null) throw new EntityNotFoundException(paramname: nameof(request.Id));

        await openingRepository.DeleteOpeningsAsync(opening);

        return new ResponseInfo(success: true, message: "opening deleted");
    }
}
