using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Openings.Queries;

public class GetAllOpeningsQueryHandler : IRequestHandler<GetAllOpeningsQuery, DataResponseInfo<List<OpeningDTO>>>
{
    private readonly IOpeningRepository openingRepository;
    private readonly IMapper mapper;
    
    public GetAllOpeningsQueryHandler(IOpeningRepository openingRepository, IMapper mapper)
    {
        this.openingRepository = openingRepository;
        this.mapper = mapper;
    }

    public async Task<DataResponseInfo<List<OpeningDTO>>> Handle(GetAllOpeningsQuery request, CancellationToken cancellationToken)
    {
        var openings = await openingRepository.GetAllOpeningsAsync();
        
        if (openings is null) throw new EntityNotFoundException();

        var openingModel = mapper.Map<List<OpeningDTO>>(openings);
        
        return new DataResponseInfo<List<OpeningDTO>>(data: openingModel, success: true, message: "all meetups");
    }
}
