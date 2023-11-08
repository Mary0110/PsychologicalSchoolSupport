using Mapster;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.OrderMeetup;
using PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Update;
using PsychologicalSupportPlatform.Meet.Application.Openings.Commands.Create;
using PsychologicalSupportPlatform.Meet.Application.Openings.Commands.Update;
using PsychologicalSupportPlatform.Meet.Domain.Entities;

namespace PsychologicalSupportPlatform.Meet.API.Mapping;

public class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Meetup, MeetupDTO>()
            .TwoWays();

        config.NewConfig<Meetup, OrderMeetupCommand>()
            .TwoWays()
            .Map(dest => dest.MeetupDto, src => src);

        config.NewConfig<MeetupDTO, UpdateMeetupCommand>()
            .TwoWays()
            .Map(dest => dest.MeetupDTO, src => src);

        config.NewConfig<MeetupDTO, UpdateMeetupCommand>()
            .TwoWays()
            .Map(dest => dest.MeetupDTO, src => src);

        config.NewConfig<AddMeetupDTO, OrderMeetupCommand>()
            .TwoWays()
            .Map(dest => dest.MeetupDto, src => src);
        
        config.NewConfig<Opening, OpeningDTO>()
            .TwoWays();

        config.NewConfig<Opening, CreateOpeningCommand>()
            .TwoWays()
            .Map(dest => dest.OpeningDto, src => src);

        config.NewConfig<OpeningDTO, UpdateOpeningCommand>()
            .TwoWays()
            .Map(dest => dest.openingDTO, src => src);

        config.NewConfig<OpeningDTO, UpdateOpeningCommand>()
            .TwoWays()
            .Map(dest => dest.openingDTO, src => src);
        
        config.NewConfig<AddOpeningDTO, AddCmdOpeningDTO>()
            .Map(dest => dest.Time, src => new TimeOnly(src.Hours, src.Minutes))
            .Map(dest => dest.PsychologistId, src => src.PsychologistId)
            .Map(dest => dest.Active, src => src.Active)
            .Map(dest => dest.Day, src => src.Day);

        config.NewConfig<OpeningDTO, CmdOpeningDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Time, src => new TimeOnly(src.Hours, src.Minutes))
            .Map(dest => dest.PsychologistId, src => src.PsychologistId)
            .Map(dest => dest.Active, src => src.Active)
            .Map(dest => dest.Day, src => src.Day);

        config.NewConfig<AddCmdOpeningDTO, CreateOpeningCommand>()
            .TwoWays()
            .Map(dest => dest.OpeningDto, src => src);
        
        config.NewConfig<CmdOpeningDTO, UpdateOpeningCommand>()
            .TwoWays()
            .Map(dest => dest.openingDTO, src => src);
    }
}
