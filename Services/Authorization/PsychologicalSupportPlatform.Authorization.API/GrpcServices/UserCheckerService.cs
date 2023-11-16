using Grpc.Core;
using PsychologicalSupportPlatform.Authorization.API.Protos;
using PsychologicalSupportPlatform.Authorization.Application.Interfaces;

namespace PsychologicalSupportPlatform.Authorization.API.GrpcServices;

public class UserCheckerService : UserChecker.UserCheckerBase
{
    private readonly IUserRepository repository;

    public UserCheckerService(IUserRepository repository)
    {
        this.repository = repository;
    }

    public override async Task<UserReply> CheckUser(UserRequest request, ServerCallContext context)
    {
        var record = await repository.GetUserByIdAsync(request.UserId);
        bool exists = record != null;
        
        return await Task.FromResult(new UserReply
        {
            Exists = exists,
            Role = record!.Role
        });
    }
}
