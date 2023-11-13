using Microsoft.Extensions.Options;
using PsychologicalSupportPlatform.Messaging.Domain.Entities;
using MongoDB.Driver;
using PsychologicalSupportPlatform.Messaging.Application;

namespace PsychologicalSupportPlatform.Messaging.Infrastructure.Data;

public class MessageRepository: IMessageRepository
{
    private readonly IMongoCollection<Room> mesCollection;

    public MessageRepository(IOptions<ChatDbConfig> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionURI);
        var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
        mesCollection = mongoDatabase.GetCollection<Room>(options.Value.CollectionName);

        var indexOptions = new CreateIndexOptions { Unique = true };
        var mesBuilder = Builders<Message>.IndexKeys;
        var indexModel = new CreateIndexModel<Message>(mesBuilder.Ascending(x => x.Id));
        mesCollection.Indexes.CreateOne(indexModel);
    }

    public async Task<List<Message>> GetAsync()
    {
        return await mesCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Message?> GetAsync(string mesId)
    {
        return await mesCollection.Find(x => x.Id == mesId).FirstOrDefaultAsync();
    }

    public async Task AddAsync(Message newMes)
    {
        await mesCollection.InsertOneAsync(newMes);
    }

    public async Task UpdateAsync(string id, Message updatedMes)
    {
        await mesCollection.ReplaceOneAsync(x => x.Id == id, updatedMes);
    }

    public async Task RemoveAsync(string id)
    {
        await mesCollection.DeleteOneAsync(x => x.Id == id);
    }
}