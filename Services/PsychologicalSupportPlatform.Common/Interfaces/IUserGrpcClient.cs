using PsychologicalSupportPlatform.Common.Protos;

namespace PsychologicalSupportPlatform.Common.Interfaces;

public interface IUserGrpcClient
{
    Task<UserReply> CheckUserAsync(int userId, CancellationToken cancellationToken = default);
}
