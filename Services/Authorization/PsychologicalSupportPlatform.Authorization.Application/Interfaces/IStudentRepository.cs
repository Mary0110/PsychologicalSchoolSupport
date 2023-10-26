using PsychologicalSupportPlatform.Authorization.Domain.Entities;

namespace PsychologicalSupportPlatform.Authorization.Application.Interfaces;

public interface IStudentRepository
{
    Task<List<Student>> GetAllStudentsAsync(int pageNumber, int pageSize);
    
    Task<List<Student>> GetStudentsByFormAsync(Form form);

    Task<List<Student>> GetStudentsByParallelAsync(int num);

    Task EditStudentAsync(Student student);
    
    Task<Student?> GetStudentByIdAsync(int id);

    Task<Student?> GetStudentByEmailAsync(string email);

    Task<Student?> AuthenticateStudentAsync(string email, string password);

    Task RegisterStudentAsync(Student student);

    Task DeleteStudentAsync(int id);
}
