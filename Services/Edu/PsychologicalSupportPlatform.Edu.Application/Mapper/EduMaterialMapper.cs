using Mapster;
using PsychologicalSupportPlatform.Edu.Application.DTOs;
using PsychologicalSupportPlatform.Edu.Domain.Entities;

namespace PsychologicalSupportPlatform.Edu.Application.Mapper;

public class EduMaterialMapper: IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<EduMaterialDTO, EduMaterial>()
            .TwoWays()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Theme, src => src.Theme);

        config.NewConfig<AddEduMaterialDTO, EduMaterial>()
            .TwoWays()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Theme, src => src.Theme);
    }
}
