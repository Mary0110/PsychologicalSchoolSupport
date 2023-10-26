using PsychologicalSupportPlatform.Authorization.Domain.DTOs;
using PsychologicalSupportPlatform.Authorization.Domain.Entities;
using PsychologicalSupportPlatform.Common;

namespace PsychologicalSupportPlatform.Authorization.Application.Interfaces;

public interface IFormService
{
    Task<DataResponseInfo<List<AddFormDTO>>> GetAllFormsAsync(int pageNumber, int pageSize);

    Task<DataResponseInfo<List<AddFormDTO>>> GetFormsByParallelAsync(int num, int pageNumber, int pageSize);
    
    Task<ResponseInfo> DeleteFormAsync(AddFormDTO formDTO);

    Task<ResponseInfo> UpdateFormAsync(AddFormDTO form);
    
    Task<ResponseInfo> AddFormAsync(AddFormDTO form);
}
