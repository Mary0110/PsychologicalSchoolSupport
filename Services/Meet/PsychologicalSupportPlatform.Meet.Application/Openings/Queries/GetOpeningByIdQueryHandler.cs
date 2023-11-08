using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Openings.Queries;

public class GetOpeningByIdHandler : IRequestHandler<GetOpeningByIdQuery, DataResponseInfo<OpeningDTO>>
{
    private readonly IOpeningRepository openingRepository;
    private readonly IMapper mapper;

    public GetOpeningByIdHandler(IOpeningRepository openingRepository, IMapper mapper)
    {
        this.openingRepository = openingRepository;
        this.mapper = mapper;
    }
    
    public async Task<DataResponseInfo<OpeningDTO>> Handle(GetOpeningByIdQuery request, CancellationToken cancellationToken)
    {
        var opening = await openingRepository.GetOpeningByIdAsync(request.Id);
        
        if (opening is null) return new DataResponseInfo<OpeningDTO>(data: null, success: false, message: $"no opening with id {request.Id}");

        var openingModel = mapper.Map<OpeningDTO>(opening);
        
        return new DataResponseInfo<OpeningDTO>(data: openingModel, success: true, message: $"opening with id {request.Id}");  
    }
}
