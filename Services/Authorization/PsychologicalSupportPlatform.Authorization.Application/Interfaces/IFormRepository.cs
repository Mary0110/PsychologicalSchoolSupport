using PsychologicalSupportPlatform.Authorization.Domain.Entities;

namespace PsychologicalSupportPlatform.Authorization.Application.Interfaces;

public interface IFormRepository
{
    Task<List<Form>> GetAllFormsAsync();
    
    Task<Form?> GetFormAsync(int parallel, char letter);

    Task<List<Form>> GetFormsByParallelAsync(int num);
    
    Task DeleteFormAsync(Form form);

    Task EditFormAsync(Form form);

    Task AddFormAsync(Form form);
}