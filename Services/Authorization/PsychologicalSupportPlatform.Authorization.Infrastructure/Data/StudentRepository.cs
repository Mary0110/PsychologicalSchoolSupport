using PsychologicalSupportPlatform.Authorization.Application.Interfaces;
using PsychologicalSupportPlatform.Authorization.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PsychologicalSupportPlatform.Common;

namespace PsychologicalSupportPlatform.Authorization.Infrastructure.Data;

public class StudentRepository : IStudentRepository
{
    private readonly DataContext _context;

    public StudentRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<List<Student>> GetAllStudentsAsync(int pageNumber, int pageSize)
    {
        return _context.Students.AsNoTracking().ToPagedCollection(pageNumber, pageSize).ToList();
    }

    public async Task<List<Student>> GetStudentsByFormAsync(Form form, int pageNumber, int pageSize)
    {
        return _context.Students.AsNoTracking().Where(p => p.Parallel == form.Parallel && p.Letter == form.Letter)
            .ToPagedCollection(pageNumber, pageSize).ToList();
    }

    public async Task<List<Student>> GetStudentsByParallelAsync(int num, int pageNumber, int pageSize)
    {
        return _context.Students.Where(p => p.Parallel == num).AsNoTracking() 
            .ToPagedCollection(pageNumber, pageSize).ToList();
    }

    public async Task EditStudentAsync(Student student)
    {
        var prevUser = await GetStudentByIdAsync(student.Id);

        if (prevUser is not null)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }
    }    

    public async Task<Student?> GetStudentByIdAsync(int id)
    {
        return await _context.Students.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Student?> GetStudentByEmailAsync(string email)
    {
        return await _context.Students.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Student?> AuthenticateStudentAsync(string email, string password)
    {
        return await _context.Students.AsNoTracking().FirstOrDefaultAsync(p => p.Email == email && p.Password == password);
    }

    public async Task RegisterStudentAsync(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteStudentAsync(int id)
    {
        var user = await GetStudentByIdAsync(id);

        if (user is not null)
        {
            _context.Students.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
