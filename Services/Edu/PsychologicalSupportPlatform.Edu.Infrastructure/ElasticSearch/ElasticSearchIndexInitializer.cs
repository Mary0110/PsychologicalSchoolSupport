using MapsterMapper;
using Nest;
using PsychologicalSupportPlatform.Edu.Application.DTOs;
using PsychologicalSupportPlatform.Edu.Application.Interfaces;

namespace PsychologicalSupportPlatform.Edu.Infrastructure.ElasticSearch;

public class ElasticSearchIndexInitializer
{
    private IElasticClient _elasticClient;
    private IEduMaterialRepository _repository;
    private IMapper _mapper;

    public ElasticSearchIndexInitializer(
        IElasticClient elasticClient,
        IEduMaterialRepository repository,
        IMapper mapper)
    {
        _elasticClient = elasticClient;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task Initialize()
    {
        var result = await _elasticClient.Indices.ExistsAsync("eduMaterial");
        
        if (!result.Exists)
        {
            CreateIndex("eduMaterial");
        }
    }

    private void CreateIndex(string indexName)
    {
        var createIndexResponse = _elasticClient.Indices.Create(indexName,
            index => index.Map<AddEduMaterialDTO>(typeDescr => typeDescr
                .AutoMap()
                .Properties(propertyDescr => propertyDescr
                    .Text(textPropertyDescriptor => textPropertyDescriptor
                        .Name(n => n.Name)
                        .Analyzer("standard")
                    )
                    .Text(t => t
                        .Name(n => n.Theme)
                        .Analyzer("standard")
                    )
                )
            ));
    }
}