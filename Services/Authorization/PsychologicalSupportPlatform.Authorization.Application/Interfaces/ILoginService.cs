using PsychologicalSupportPlatform.Authorization.Domain.DTOs;
using PsychologicalSupportPlatform.Authorization.Domain.Entities;
using PsychologicalSupportPlatform.Common;

namespace PsychologicalSupportPlatform.Authorization.Application.Interfaces;

public interface ILoginService
{
    Task<DataResponseInfo<string>> GetTokenAsync(LoginData data);

    Task<DataResponseInfo<List<User>>> GetAllUsersAsync(int pageNumber, int pageSize);
    
    Task<DataResponseInfo<List<AddStudentDTO>>> GetAllStudentsAsync(int pageNumber, int pageSize);

    Task<DataResponseInfo<User>> GetUserByIdAsync(int id);
    
    Task<DataResponseInfo<AddStudentDTO>> GetStudentByIdAsync(int id);
    
    Task<ResponseInfo> RegisterUserAsync(AddUserDTO user);
    
    Task<ResponseInfo> RegisterStudentAsync(AddStudentDTO user);

    Task<ResponseInfo> DeleteUserAsync(int id);
    
    Task<ResponseInfo> DeleteStudentAsync(int id);
    
    Task<ResponseInfo> UpdateUserAsync(UpdateUserDTO user);
    
    Task<ResponseInfo> UpdateStudentAsync(UpdateStudentDTO user);

    Task<DataResponseInfo<List<AddStudentDTO>>> GetStudentsByFormAsync(AddFormDTO form, int pageNumber, int pageSize);

    Task<DataResponseInfo<List<AddStudentDTO>>> GetStudentsByParallelAsync(int num, int pageNumber, int pageSize);
}
