using Mapster;
using PsychologicalSupportPlatform.Meet.Application.DTOs.Meetup;
using PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Approve;
using PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.OrderMeetup;
using PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.OrderMeetupByPsychologist;
using PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Update;
using PsychologicalSupportPlatform.Meet.Domain.Entities;

namespace PsychologicalSupportPlatform.Meet.Application.Mapping;

public class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AddMeetupDTO, OrderMeetupCommand>()
            .TwoWays()
            .Map(dest => dest.MeetupDto, src => src);
        
        config.NewConfig<Meetup, OrderMeetupCommand>()
            .TwoWays()
            .Map(dest => dest.MeetupDto, src => src);

        config.NewConfig<Meetup, OrderMeetupByPsychologistCommand>()
            .TwoWays()
            .Map(dest => dest.MeetupDto, src => src);

        config.NewConfig<Meetup, AddMeetupDTO>()
            .TwoWays()
            .Map(dest => dest, src => src);

        config.NewConfig<ApproveMeetupDTO, ApproveMeetupByStudentCommand>()
            .TwoWays()
            .Map(dest => dest.MeetupDTO, src => src);

        config.NewConfig<MeetupDTO, UpdateMeetupCommand>()
            .TwoWays()
            .Map(dest => dest.MeetupDTO, src => src);

        config.NewConfig<AddMeetupDTO, UpdateMeetupCommand>()
            .TwoWays()
            .Map(dest => dest.MeetupDTO, src => src);
    }
}
