using Mapster;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Report.Domain.Entities;

namespace PsychologicalSupportPlatform.Report.Application.Mapping;

public class Mapper: IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<MeetupMessageObject, MeetupReport>()
            .TwoWays()
            .Map(dest => dest.StudentId, src => src.StudentId)
            .Map(dest => dest.DateTime, src => src.Date)
            .Map(dest => dest.MeetupId, src => src.MeetupId);
    }
}
