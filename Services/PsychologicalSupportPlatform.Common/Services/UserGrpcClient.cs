using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Common.Interfaces;
using PsychologicalSupportPlatform.Common.Protos;

namespace PsychologicalSupportPlatform.Common.Services;

public class UserGrpcClient : IUserGrpcClient
{
    private readonly UserChecker.UserCheckerClient userCheckerClient;

    public UserGrpcClient(UserChecker.UserCheckerClient userCheckerClient)
    {
        this.userCheckerClient = userCheckerClient;
    }

    public async Task<UserReply> CheckUserAsync(int userId, CancellationToken cancellationToken)
    {
        var reply = await userCheckerClient.CheckUserAsync(new UserRequest { UserId = userId }, cancellationToken: cancellationToken);
        
        if (!reply.Exists)
        {
            throw new EntityNotFoundException(nameof(userId));
        }

        return reply;
    }
}
