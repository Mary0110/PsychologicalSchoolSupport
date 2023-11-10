using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Openings.Queries;

public class GetOpeningsByDayOfWeekHandler : IRequestHandler<GetOpeningsByDayOfWeekQuery, DataResponseInfo<List<OpeningDTO>>>
{
    private readonly IOpeningRepository openingRepository;
    private readonly IMapper mapper;

    public GetOpeningsByDayOfWeekHandler(IOpeningRepository openingRepository, IMapper mapper)
    {
        this.openingRepository = openingRepository;
        this.mapper = mapper;
    }
    
    public async Task<DataResponseInfo<List<OpeningDTO>>> Handle(GetOpeningsByDayOfWeekQuery request, CancellationToken cancellationToken)
    {
        var openings = await openingRepository.GetOpeningsByDayOfWeekAsync(request.DayOfWeek);
        
        if (openings is null) throw new EntityNotFoundException(nameof(request.DayOfWeek));
        
        var openingModel = mapper.Map<List<OpeningDTO>>(openings);
        
        return new DataResponseInfo<List<OpeningDTO>>(data: openingModel, success: true, message: $"openings on {request.DayOfWeek}");    
    }
}
