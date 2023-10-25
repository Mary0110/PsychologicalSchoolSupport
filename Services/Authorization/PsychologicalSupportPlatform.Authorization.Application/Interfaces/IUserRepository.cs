using PsychologicalSupportPlatform.Authorization.Domain.Entities;

namespace PsychologicalSupportPlatform.Authorization.Application.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllUsersAsync();
    
    Task<User?> GetUserByIdAsync(int id);

    Task<User?> GetUserByEmailAsync(string email);

    Task<User?> AuthenticateUserAsync(string email, string password);

    Task RegisterUserAsync(User user);

    Task DeleteUserAsync(int id);

    Task EditUserAsync(User user);
}