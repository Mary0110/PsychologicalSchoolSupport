using System.Reflection;
using Mapster;

namespace PsychologicalSupportPlatform.Messaging.API.Extensions
{
    public class GetConfiguredMapping
    {
        public static TypeAdapterConfig GetConfiguredMappingConfig()
        {
            var cfg = TypeAdapterConfig.GlobalSettings;
            cfg.Scan(Assembly.GetExecutingAssembly(), Meet.Application.AssemblyReference.Assembly);
            return cfg;
        }
    }
}
