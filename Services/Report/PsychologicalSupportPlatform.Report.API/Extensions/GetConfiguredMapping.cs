using System.Reflection;
using Mapster;

namespace PsychologicalSupportPlatform.Report.API.Extensions
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
