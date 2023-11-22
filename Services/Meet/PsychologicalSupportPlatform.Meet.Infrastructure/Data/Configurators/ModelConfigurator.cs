using Microsoft.EntityFrameworkCore;
using PsychologicalSupportPlatform.Meet.Infrastructure.Data.Comparers;
using PsychologicalSupportPlatform.Meet.Infrastructure.Data.Converters;

namespace PsychologicalSupportPlatform.Meet.Infrastructure.Data.Configurators;

public static class ModelConfigurator
{
    public static void ApplyConvention(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter, DateOnlyComparer>()
            .HaveColumnType("date");
        builder.Properties<TimeOnly>()
            .HaveConversion<TimeOnlyConverter, TimeOnlyComparer>();
    }
}