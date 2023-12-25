using Microsoft.Extensions.Options;
using Nest;
using PsychologicalSupportPlatform.Edu.Application.DTOs;
using PsychologicalSupportPlatform.Edu.Infrastructure.Config;

namespace PsychologicalSupportPlatform.Edu.Infrastructure.Data.ElasticSearch;

public class ElasticSearchIndexInitializer
{
    private readonly IElasticClient _elasticClient;
    private readonly ElasticSearchConfig _elasticSearchConfig;

    public ElasticSearchIndexInitializer(IOptions<ElasticSearchConfig> elasticSearchConfig,
        IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
        _elasticSearchConfig = elasticSearchConfig.Value;
    }

    public async Task Initialize()
    {
        var indexName = _elasticSearchConfig.Index;
        var result = await _elasticClient.Indices.ExistsAsync(indexName); 

        if (!result.Exists)
        {
            var createIndexResponse = _elasticClient.Indices.Create(indexName,
                index => index.Map<EduMaterialDTO>(typeDescr => typeDescr
                    .AutoMap()
                    .Properties(ps => ps
                        .Text(s => s
                            .Name(n => n.Name)
                            .Analyzer("rus_en_analyzer")
                        )
                        .Text(s => s
                            .Name(n => n.Theme)
                            .Analyzer("rus_en_analyzer")
                        )
                    ))
                    .Settings(settings => settings
                        .Analysis(analysis => analysis
                            .Analyzers(analyzers => analyzers
                                .Custom("rus_en_analyzer", custom => custom
                                        .Tokenizer("standard")
                                        .Filters("lowercase", "russian_stop", "english_stop")
                                )
                            )
                            .TokenFilters(tokenFilters => tokenFilters
                                .Stop("russian_stop", stop => stop
                                        .StopWords("_russian_")
                                )
                                .Stop("english_stop", stop => stop
                                        .StopWords("_english_")
                                )
                            )
                        )
                        .NumberOfReplicas(0)
                    )
            );
        }
    }
}