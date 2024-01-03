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

                config.NewConfig<CreateScheduleCellDTO, ScheduleCell>()
                        .Map(dest => dest.Active, 
                                src => src.Active)
                        .Map(dest => dest.Day, 
                                src => src.Day)
                        .Map(dest => dest.PsychologistId, 
                                src => src.PsychologistId)
                        .Map(dest => dest.Time, 
                                src => new TimeOnly(src.Hours, src.Minutes));

                config.NewConfig<ScheduleCellDTO, ScheduleCell>()
                        .Map(dest => dest.Time, 
                                src => new TimeOnly(src.Hours, src.Minutes));
                
                config.NewConfig<CreateScheduleCellDTO, UpdateScheduleCellCommand>()
                        .Map(dest => dest.ScheduleCellDto,
                                src => src);

                config.NewConfig<CreateScheduleCellDTO, CreateScheduleCellCommand>()
                        .Map(dest => dest.ScheduleCellDto,
                                src => src);
                
                config.NewConfig<CreateScheduleCellCommand, ScheduleCell>()
                        .Map(dest => dest.Active,
                                src => src.ScheduleCellDto.Active)
                        .Map(dest => dest.Day, 
                                src => src.ScheduleCellDto.Day)
                        .Map(dest => dest.Time, src => 
                                new TimeOnly(src.ScheduleCellDto.Hours, src.ScheduleCellDto.Minutes))
                        .Map(dest => dest.PsychologistId, 
                                src => src.ScheduleCellDto.PsychologistId);
              
                config.NewConfig<UpdateScheduleCellCommand, ScheduleCell>()
                        .Map(dest => dest.Active, 
                                src => src.ScheduleCellDto.Active)
                        .Map(dest => dest.Day, 
                                src => src.ScheduleCellDto.Day)
                        .Map(dest => dest.Time, src => 
                                new TimeOnly(src.ScheduleCellDto.Hours, src.ScheduleCellDto.Minutes))
                        .Map(dest => dest.PsychologistId, 
                                src => src.ScheduleCellDto.PsychologistId);
        }
}
