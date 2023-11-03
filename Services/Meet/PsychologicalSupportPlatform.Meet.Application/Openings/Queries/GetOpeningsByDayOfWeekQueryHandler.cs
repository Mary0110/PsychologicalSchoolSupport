using MapsterMapper;
using MediatR;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Application.Exceptions;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;

namespace PsychologicalSupportPlatform.Meet.Application.Opening.Queries;

public class GetOpeningsByDayOfWeekHandler : IRequestHandler<GetOpeningsByDayOfWeekQuery, List<OpeningDTO>>
{
    private readonly IOpeningRepository openingRepository;
    private readonly IMapper mapper;

    public GetOpeningsByDayOfWeekHandler(IOpeningRepository openingRepository, IMapper mapper)
    {
        this.openingRepository = openingRepository;
        this.mapper = mapper;
    }
    
    public async Task<List<OpeningDTO>> Handle(GetOpeningsByDayOfWeekQuery request, CancellationToken cancellationToken)
    {
        var openings = await openingRepository.GetOpeningsByDayOfWeekAsync(request.DayOfWeek);
        
        if (openings == null)
        {
            throw new EntityNotFoundException("No such openings");
        }
        
        var openingModel = mapper.Map<List<OpeningDTO>>(openings);
        
        return openingModel;    
    }
}