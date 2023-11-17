using Mapster;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.OrderMeetup;
using PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Update;
using PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Commands.Create;
using PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Commands.Update;
using PsychologicalSupportPlatform.Meet.Domain.Entities;

namespace PsychologicalSupportPlatform.Meet.Application.Mapping;

public class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
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
    }
}
