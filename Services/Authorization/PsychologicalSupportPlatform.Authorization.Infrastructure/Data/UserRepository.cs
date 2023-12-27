using Microsoft.EntityFrameworkCore;
using PsychologicalSupportPlatform.Authorization.Application.Interfaces;
using PsychologicalSupportPlatform.Authorization.Domain.Entities;
using PsychologicalSupportPlatform.Common;

namespace PsychologicalSupportPlatform.Authorization.Infrastructure.Data;

public class UserRepository :IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<User?> AuthenticateUserAsync(string email, string password)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(p => p.Email == email && p.Password == password);
    }

    public async Task<List<User>> GetAllUsersAsync(int pageNumber, int pageSize)
    {
        return _context.Users.AsNoTracking().ToPagedCollection(pageNumber, pageSize).ToList();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task RegisterUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await GetUserByIdAsync(id);

        if (user is not null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }

    public async Task EditUserAsync(User user)
    {
        var prevUser = await GetUserByIdAsync(user.Id);

        if (prevUser is not null)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
