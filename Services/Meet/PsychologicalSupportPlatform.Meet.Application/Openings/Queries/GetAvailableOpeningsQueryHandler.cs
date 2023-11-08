using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Openings.Queries;

public class GetAvailableOpeningsQueryHandler: IRequestHandler<GetAvailableOpeningsQuery, DataResponseInfo<List<OpeningDTO>>>
{
    private readonly IOpeningRepository openingRepository;
    private readonly IMapper mapper;

    public GetAvailableOpeningsQueryHandler(IOpeningRepository openingRepository, IMapper mapper)
    {
        this.openingRepository = openingRepository;
        this.mapper = mapper;
    }
    
    public async Task<DataResponseInfo<List<OpeningDTO>>> Handle(GetAvailableOpeningsQuery request, CancellationToken cancellationToken)
    {
        var openings = await openingRepository.GetAvailableOpeningsAsync();
        
        if (openings is null) return new DataResponseInfo<List<OpeningDTO>>(data: null, success: false, message: "no available openings");
        
        var openingModel = mapper.Map<List<OpeningDTO>>(openings);
        
        return new DataResponseInfo<List<OpeningDTO>>(data: openingModel, success: true, message: "available openings");
    }
}
