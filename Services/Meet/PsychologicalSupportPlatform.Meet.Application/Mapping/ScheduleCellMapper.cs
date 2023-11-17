using Mapster;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Commands.Create;
using PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Commands.Update;
using PsychologicalSupportPlatform.Meet.Domain.Entities;

namespace PsychologicalSupportPlatform.Meet.Application.Mapping;

public class ScheduleCellMapper : IRegister
{ 
        public void Register(TypeAdapterConfig config)
        {
                config.NewConfig<ScheduleCell, CreateScheduleCellCommand>()
                        .TwoWays()
                        .Map(dest => dest.ScheduleCellDto, src => src);

                config.NewConfig<ScheduleCellDTO, UpdateScheduleCellCommand>()
                        .TwoWays()
                        .Map(dest => dest.scheduleCellDTO, src => src);

                config.NewConfig<ScheduleCellDTO, UpdateScheduleCellCommand>()
                        .TwoWays()
                        .Map(dest => dest.scheduleCellDTO, src => src);

                config.NewConfig<CreateScheduleCellDTO, AddScheduleCellDTO>()
                        .Map(dest => dest.Time, src => new TimeOnly(src.Hours, src.Minutes))
                        .Map(dest => dest.PsychologistId, src => src.PsychologistId)
                        .Map(dest => dest.Active, src => src.Active)
                        .Map(dest => dest.Day, src => src.Day);

                config.NewConfig<ScheduleCellDTO, UpdateScheduleCellDTO>()
                        .Map(dest => dest.Id, src => src.Id)
                        .Map(dest => dest.Time, src => new TimeOnly(src.Hours, src.Minutes))
                        .Map(dest => dest.PsychologistId, src => src.PsychologistId)
                        .Map(dest => dest.Active, src => src.Active)
                        .Map(dest => dest.Day, src => src.Day);

                config.NewConfig<AddScheduleCellDTO, CreateScheduleCellCommand>()
                        .TwoWays()
                        .Map(dest => dest.ScheduleCellDto, src => src);

                config.NewConfig<UpdateScheduleCellDTO, UpdateScheduleCellCommand>()
                        .TwoWays()
                        .Map(dest => dest.scheduleCellDTO, src => src);
        }
}