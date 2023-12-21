using Mapster;
using PsychologicalSupportPlatform.Meet.Application.DTOs.ScheduleCell;
using PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Commands.Create;
using PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Commands.Update;
using PsychologicalSupportPlatform.Meet.Domain.Entities;

namespace PsychologicalSupportPlatform.Meet.Application.Mapping;

public class ScheduleCellMapper : IRegister
{ 
        public void Register(TypeAdapterConfig config)
        {
                config.NewConfig<ScheduleCell, ScheduleCellDTO>()
                        .Map(dest => dest.Hours, src => src.Time.Hour)
                        .Map(dest => dest.Minutes, src => src.Time.Minute);

                config.NewConfig<AddScheduleCellDTO, ScheduleCell>()
                        .Map(dest => dest, src => src);

                config.NewConfig<ScheduleCellDTO, ScheduleCell>()
                        .Map(dest => dest.Time, src => new TimeOnly(src.Hours, src.Minutes));

                config.NewConfig<ScheduleCellDTO, UpdateScheduleCellCommand>()
                        .TwoWays()
                        .Map(dest => dest.ScheduleCellDto, src => src);

                config.NewConfig<ScheduleCellDTO, UpdateScheduleCellCommand>()
                        .TwoWays()
                        .Map(dest => dest.ScheduleCellDto, src => src);

                config.NewConfig<CreateScheduleCellDTO, AddScheduleCellDTO>()
                        .Map(dest => dest.Time, src => new TimeOnly(src.Hours, src.Minutes))
                        .Map(dest => dest.PsychologistId, src => src.PsychologistId)
                        .Map(dest => dest.Active, src => src.Active)
                        .Map(dest => dest.Day, src => src.Day);
                
                config.NewConfig<AddScheduleCellDTO, UpdateScheduleCellCommand>()
                        .TwoWays()
                        .Map(dest => dest.ScheduleCellDto, src => src);
                
                config.NewConfig<CreateScheduleCellDTO, CreateScheduleCellCommand>()
                        .TwoWays()
                        .Map(dest => dest.ScheduleCellDto, src => src);
        }
}
