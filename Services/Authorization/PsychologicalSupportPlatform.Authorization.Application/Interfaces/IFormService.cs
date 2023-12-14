using PsychologicalSupportPlatform.Authorization.Domain.DTOs;
using PsychologicalSupportPlatform.Authorization.Domain.Entities;
using PsychologicalSupportPlatform.Common;

namespace PsychologicalSupportPlatform.Authorization.Application.Interfaces;

public interface IFormService
{
    Task<DataResponseInfo<List<AddFormDTO>>> GetAllFormsAsync(int pageNumber, int pageSize);

    Task<DataResponseInfo<List<AddFormDTO>>> GetFormsByParallelAsync(int num, int pageNumber, int pageSize);
    
    Task<ResponseInfo> DeleteFormAsync(int formNum, char formLetter);

    Task<ResponseInfo> UpdateFormAsync(AddFormDTO form);
    
    Task<ResponseInfo> AddFormAsync(int num, char letter);
}
