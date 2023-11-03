using PsychologicalSupportPlatform.Authorization.Domain.Entities;

namespace PsychologicalSupportPlatform.Authorization.Application.Interfaces;

public interface IFormRepository
{
    Task<List<Form>> GetAllFormsAsync(int pageNumber, int pageSize);
    
    Task<Form?> GetFormAsync(int parallel, char letter);

    Task<List<Form>> GetFormsByParallelAsync(int num, int pageNumber, int pageSize);
    
    Task DeleteFormAsync(Form form);

    Task EditFormAsync(Form form);

    Task AddFormAsync(Form form);
}
