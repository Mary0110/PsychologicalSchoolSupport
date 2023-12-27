using Grpc.Core;
using PsychologicalSupportPlatform.Authorization.API.Protos;
using PsychologicalSupportPlatform.Authorization.Application.Interfaces;

namespace PsychologicalSupportPlatform.Authorization.API.GrpcServices;

public class UserCheckerService : UserChecker.UserCheckerBase
{
    private readonly IUserRepository _repository;

    public UserCheckerService(IUserRepository repository)
    {
        _repository = repository;
    }

    public override async Task<UserReply> CheckUser(UserRequest request, ServerCallContext context)
    {
        var record = await _repository.GetUserByIdAsync(request.UserId);
        bool exists = record != null;
        
        return await Task.FromResult(new UserReply
        {
            Exists = exists,
            Role = record!.Role
        });
    }
    
    public override async Task<UserNameReply> GetUserName(UserNameRequest request, ServerCallContext context)
    {
        var record = await _repository.GetUserByIdAsync(request.UserId);
        
        return await Task.FromResult(new UserNameReply
        {
            Name = record.Name,
            Surname = record.Surname,
            Patronymic = record.Patronymic
        });
    }
}
