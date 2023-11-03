using Mapster;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Create;
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

        config.NewConfig<Meetup, CreateMeetupCommand>()
            .TwoWays()
            .Map(dest => dest.meetupDto, src => src);

        config.NewConfig<MeetupDTO, UpdateMeetupCommand>()
            .TwoWays()
            .Map(dest => dest.MeetupDTO, src => src);

        config.NewConfig<MeetupDTO, UpdateMeetupCommand>()
            .TwoWays()
            .Map(dest => dest.MeetupDTO, src => src);

        config.NewConfig<AddMeetupDTO, CreateMeetupCommand>()
            .TwoWays()
            .Map(dest => dest.meetupDto, src => src);
        
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

        config.NewConfig<AddOpeningDTO, CreateOpeningCommand>()
            .TwoWays()
            .Map(dest => dest.OpeningDto, src => src);
    }
}
