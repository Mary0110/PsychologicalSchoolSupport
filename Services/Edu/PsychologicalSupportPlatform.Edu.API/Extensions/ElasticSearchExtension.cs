using Nest;
using PsychologicalSupportPlatform.Edu.Infrastructure.Config;
using PsychologicalSupportPlatform.Edu.Infrastructure.Data.ElasticSearch;

namespace PsychologicalSupportPlatform.Edu.API.Extensions;

public static class ElasticSearchExtension
{
    public static void AddElasticSearch(this IServiceCollection services,
            IConfiguration configuration)
        {
            var section = configuration.GetSection(key: nameof(ElasticSearchConfig));
            services.Configure<ElasticSearchConfig>(section);
            var elasticSearchConfig = section.Get<ElasticSearchConfig>();
            
            services.AddScoped<ElasticSearchIndexInitializer>();

            var settings = new ConnectionSettings(new Uri(elasticSearchConfig.Url))
                .DefaultIndex(elasticSearchConfig.Index).DisableDirectStreaming();
            settings.PrettyJson();
            
            var client = new ElasticClient(settings);
            services.AddSingleton<IElasticClient>(client);
        }
}
