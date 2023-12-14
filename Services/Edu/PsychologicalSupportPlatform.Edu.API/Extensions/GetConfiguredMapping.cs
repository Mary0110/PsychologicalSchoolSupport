using Mapster;
using System.Reflection;

namespace PsychologicalSupportPlatform.Edu.API.Extensions
{
    public class GetConfiguredMapping
    {
        public static TypeAdapterConfig GetConfiguredMappingConfig()
        {
            var adapterConfig = TypeAdapterConfig.GlobalSettings;
            adapterConfig.Scan(Assembly.GetExecutingAssembly(), Application.AssemblyReference.Assembly);
            
            return adapterConfig;
        }
    }
}
