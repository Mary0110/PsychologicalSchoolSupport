using Microsoft.Extensions.Options;
using PsychologicalSupportPlatform.Messaging.Domain.Entities;
using MongoDB.Driver;
using PsychologicalSupportPlatform.Messaging.Application;

namespace PsychologicalSupportPlatform.Messaging.Infrastructure.Data;

public class MessageRepository: IMessageRepository
{
    private readonly IMongoCollection<Message> mesCollection;

    public MessageRepository(IOptions<ChatDbConfig> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
        mesCollection = mongoDatabase.GetCollection<Message>(options.Value.ChatsCollectionName);

        var indexOptions = new CreateIndexOptions { Unique = true };
        var mesBuilder = Builders<Message>.IndexKeys;
        var indexModel = new CreateIndexModel<Message>(mesBuilder.Ascending(x => x.Id));
        mesCollection.Indexes.CreateOne(indexModel);
    }

    public async Task<List<Message>> GetAsync()
    {
        return await mesCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Message?> GetAsync(int mesId)
    {
        return await mesCollection.Find(x => x.Id == mesId).FirstOrDefaultAsync();
    }

    public async Task AddAsync(Message newMes)
    {
        await mesCollection.InsertOneAsync(newMes);
    }

    public async Task UpdateAsync(int id, Message updatedMes)
    {
        await mesCollection.ReplaceOneAsync(x => x.Id == id, updatedMes);
    }

    public async Task RemoveAsync(int id)
    {
        await mesCollection.DeleteOneAsync(x => x.Id == id);
    }
}