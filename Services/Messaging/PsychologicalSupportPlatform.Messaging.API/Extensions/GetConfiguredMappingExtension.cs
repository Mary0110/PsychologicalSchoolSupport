﻿using System.Reflection;
using Mapster;

namespace PsychologicalSupportPlatform.Messaging.API.Extensions
{
    public class GetConfiguredMapping
    {
        public static TypeAdapterConfig GetConfiguredMappingConfig()
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly(), Messaging.Application.AssemblyReference.Assembly);
            
            return config;
        }
    }
}
