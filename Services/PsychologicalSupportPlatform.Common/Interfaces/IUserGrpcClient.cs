using PsychologicalSupportPlatform.Common.Protos;

namespace PsychologicalSupportPlatform.Common.Interfaces;

public interface IUserGrpcClient
{
    Task<UserReply> CheckUserAsync(int userId, CancellationToken token);
    
    Task<UserNameReply> CheckUserNameAsync(int userId, CancellationToken token);
}
