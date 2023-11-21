using Microsoft.Extensions.Options;
using PsychologicalSupportPlatform.Messaging.Domain.Entities;
using MongoDB.Driver;
using PsychologicalSupportPlatform.Messaging.Application;
using PsychologicalSupportPlatform.Messaging.Application.Interfaces;

namespace PsychologicalSupportPlatform.Messaging.Infrastructure.Data;

public class ChatRepository: IChatRepository
{
    private readonly ApplicationDbContext _context;
    
    public ChatRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Message>> GetAsync()
    {
        return await _context.MesCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Message?> GetAsync(string mesId)
    {
        return await _context.MesCollection.Find(mes => mes.Id == mesId).FirstOrDefaultAsync();
    }

    public async Task AddAsync(Message newMes, CancellationToken token)
    {
        await _context.MesCollection.InsertOneAsync(newMes, token);
    }

    public async Task UpdateAsync(string id, Message updatedMes)
    {
        await _context.MesCollection.ReplaceOneAsync(x => x.Id == id, updatedMes);
    }

    public async Task RemoveAsync(string id)
    {
        await _context.MesCollection.DeleteOneAsync(x => x.Id == id);
    }

    public async Task<List<Message>> GetChatHistoryAsync(string getLoggedInUserId, string otherUserId, int pageNumber, int pageSize, CancellationToken token)
    {
        return await _context.MesCollection.Find(
            c => c.ConsumerId == otherUserId && c.SenderId == getLoggedInUserId
            ).Sort(Builders<Message>.Sort.Descending("DateTime")).
            Skip((pageNumber - 1) * pageSize).Limit(pageSize)
            .ToListAsync(token);
    }
}
