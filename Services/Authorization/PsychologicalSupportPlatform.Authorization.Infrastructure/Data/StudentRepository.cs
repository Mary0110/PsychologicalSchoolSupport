using PsychologicalSupportPlatform.Authorization.Application.Interfaces;
using PsychologicalSupportPlatform.Authorization.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PsychologicalSupportPlatform.Authorization.Infrastructure.Data;

public class StudentRepository : IStudentRepository
{
    private readonly DataContext context;

    public StudentRepository(DataContext context)
    {
        this.context = context;
    }
    
    public async Task<List<Student>> GetAllStudentsAsync(int pageNumber, int pageSize)
    {
        return context.Students.AsNoTracking().ToPagedCollection(pageNumber, pageSize).ToList();
    }

    public async Task<List<Student>> GetStudentsByFormAsync(Form form, int pageNumber, int pageSize)
    {
        return context.Students.AsNoTracking().Where(p => p.Parallel == form.Parallel && p.Letter == form.Letter)
            .ToPagedCollection(pageNumber, pageSize).ToList();
    }

    public async Task<List<Student>> GetStudentsByParallelAsync(int num, int pageNumber, int pageSize)
    {
        return context.Students.Where(p => p.Parallel == num).AsNoTracking() 
            .ToPagedCollection(pageNumber, pageSize).ToList();
    }

    public async Task EditStudentAsync(Student student)
    {
        var prevUser = await GetStudentByIdAsync(student.Id);

        if (prevUser is not null)
        {
            context.Students.Update(student);
            await context.SaveChangesAsync();
        }
    }    

    public async Task<Student?> GetStudentByIdAsync(int id)
    {
        return await context.Students.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Student?> GetStudentByEmailAsync(string email)
    {
        return await context.Students.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Student?> AuthenticateStudentAsync(string email, string password)
    {
        return await context.Students.AsNoTracking().FirstOrDefaultAsync(p => p.Email == email && p.Password == password);
    }

    public async Task RegisterStudentAsync(Student student)
    {
        context.Students.Add(student);
        await context.SaveChangesAsync();
    }

    public async Task DeleteStudentAsync(int id)
    {
        var user = await GetStudentByIdAsync(id);

        if (user is not null)
        {
            context.Students.Remove(user);
            await context.SaveChangesAsync();
        }
    }
}