using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Application.Exceptions;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Opening.Queries;

public class GetAllOpeningsQueryHandler : IRequestHandler<GetAllOpeningsQuery, List<OpeningDTO>>
{
    private readonly IOpeningRepository openingRepository;
    private readonly IMapper mapper;
    
    public GetAllOpeningsQueryHandler(IOpeningRepository openingRepository, IMapper mapper)
    {
        this.openingRepository = openingRepository;
        this.mapper = mapper;
    }

    public async Task<List<OpeningDTO>> Handle(GetAllOpeningsQuery request, CancellationToken cancellationToken)
    {
        var openings = await openingRepository.GetAllOpeningsAsync();
        
        if (openings == null)
        {
            throw new EntityNotFoundException("No openings");
        }
        
        var openingModel = mapper.Map<List<OpeningDTO>>(openings);
        
        return openingModel;
    }
}