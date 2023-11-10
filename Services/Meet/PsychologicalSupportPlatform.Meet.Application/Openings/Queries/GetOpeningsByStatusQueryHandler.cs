using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Openings.Queries;

public class GetOpeningsByStatusHandler : IRequestHandler<GetOpeningsByStatusQuery, DataResponseInfo<List<OpeningDTO>>>
{
    private readonly IOpeningRepository openingRepository;
    private readonly IMapper mapper;

    public GetOpeningsByStatusHandler(IOpeningRepository openingRepository, IMapper mapper)
    {
        this.openingRepository = openingRepository;
        this.mapper = mapper;
    }
    
    public async Task<DataResponseInfo<List<OpeningDTO>>> Handle(GetOpeningsByStatusQuery request, CancellationToken cancellationToken)
    {
        var openings = await openingRepository.GetOpeningsByStatusAsync(request.Active);
        
        if (openings is null) throw new EntityNotFoundException(nameof(request.Active));

        var openingModel = mapper.Map<List<OpeningDTO>>(openings);
        
        return new DataResponseInfo<List<OpeningDTO>>(data: openingModel, success: true, message: $"no openings with active status {request.Active}");
    }
}
