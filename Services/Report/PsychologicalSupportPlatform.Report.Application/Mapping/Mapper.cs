using Mapster;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Report.Application.DTOs;
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
        
        config.NewConfig<ReportDTO, MeetupReport>()
            .TwoWays()
            .Map(dest => dest.StudentId, src => src.StudentId)
            .Map(dest => dest.DateTime, src => src.DateTime)
            .Map(dest => dest.MeetupId, src => src.MeetupId);
        
        config.NewConfig<AddMonthlyReportDTO, MonthlyReport>()
            .Map(dest => dest.Date, src => DateTime.Today);
    }
}
