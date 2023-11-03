using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Application.Exceptions;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Opening.Queries;

public class GetOpeningByIdHandler : IRequestHandler<GetOpeningByIdQuery, OpeningDTO>
{
    private readonly IOpeningRepository openingRepository;
    private readonly IMapper mapper;

    public GetOpeningByIdHandler(IOpeningRepository openingRepository, IMapper mapper)
    {
        this.openingRepository = openingRepository;
        this.mapper = mapper;
    }
    
    public async Task<OpeningDTO> Handle(GetOpeningByIdQuery request, CancellationToken cancellationToken)
    {
        var opening = await openingRepository.GetOpeningByIdAsync(request.Id);
        
        if (opening == null)
        {
            throw new EntityNotFoundException("No such opening");
        }
        
        var openingModel = mapper.Map<OpeningDTO>(opening);
        
        return openingModel;    
    }
}