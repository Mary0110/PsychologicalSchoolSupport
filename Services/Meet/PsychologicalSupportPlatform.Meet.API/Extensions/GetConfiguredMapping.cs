using System.Reflection;
using Mapster;

namespace PsychologicalSupportPlatform.Meet.API.Extensions
{
    public class GetConfiguredMapping
    {
        public static TypeAdapterConfig GetConfiguredMappingConfig()
        {
            var cfg = TypeAdapterConfig.GlobalSettings;
            cfg.Scan(Assembly.GetExecutingAssembly(), Application.AssemblyReference.Assembly);
            return cfg;
        }
    }
}
