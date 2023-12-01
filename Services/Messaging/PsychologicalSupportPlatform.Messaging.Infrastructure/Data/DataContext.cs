using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PsychologicalSupportPlatform.Messaging.Domain.Entities;
using PsychologicalSupportPlatform.Messaging.Infrastructure.Config;

namespace PsychologicalSupportPlatform.Messaging.Infrastructure.Data;

public class ApplicationDbContext:DbContext
{
    public IMongoCollection<Message> MesCollection { get; }

    public ApplicationDbContext(IOptions<ChatDbConfig> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionURI);
        var database = mongoClient.GetDatabase(options.Value.DatabaseName);
        MesCollection = database.GetCollection<Message>(options.Value.CollectionName);

        CreateIndexes();
    }

    private void CreateIndexes()
    {
        var indexOptions = new CreateIndexOptions {};
        var mesBuilder = Builders<Message>.IndexKeys;
        var indexModel = new CreateIndexModel<Message>(mesBuilder.Ascending(x => x.Id), indexOptions);
        MesCollection.Indexes.CreateOne(indexModel);
    }
}
