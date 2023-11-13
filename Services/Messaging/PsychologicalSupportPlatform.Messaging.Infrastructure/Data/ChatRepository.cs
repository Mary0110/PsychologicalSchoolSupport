using Microsoft.Extensions.Options;
using PsychologicalSupportPlatform.Messaging.Domain.Entities;
using MongoDB.Driver;
using PsychologicalSupportPlatform.Messaging.Application;

namespace PsychologicalSupportPlatform.Messaging.Infrastructure.Data;

public class ChatRepository: IChatRepository
{
    private readonly IMongoCollection<Message> mesCollection;

    public ChatRepository(IOptions<ChatDbConfig> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionURI);
        var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
        mesCollection = mongoDatabase.GetCollection<Message>(options.Value.CollectionName);

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

    public async Task<List<Message>> GetChatHistoryAsync(string getLoggedInUserId, string otherUserId)
    {
        return await mesCollection.Find(
            c => c.ConsumerId == otherUserId && c.SenderId == getLoggedInUserId
            ).Sort(Builders<Message>.Sort.Descending("DateTime")).ToListAsync();
    }
}
