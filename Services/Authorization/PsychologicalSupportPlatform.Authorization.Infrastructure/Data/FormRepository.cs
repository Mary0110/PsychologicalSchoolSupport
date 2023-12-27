using PsychologicalSupportPlatform.Authorization.Application.Interfaces;
using PsychologicalSupportPlatform.Authorization.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PsychologicalSupportPlatform.Common;

namespace PsychologicalSupportPlatform.Authorization.Infrastructure.Data;

public class FormRepository : IFormRepository
{
    private readonly DataContext _context;

    public FormRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<List<Form>> GetAllFormsAsync(int pageNumber, int pageSize)
    {
        return _context.Forms.AsNoTracking().ToPagedCollection(pageNumber, pageSize).ToList();
    }

    public async Task<Form?> GetFormAsync(int parallel, char letter)
    {
        return await _context.Forms.AsNoTracking().FirstOrDefaultAsync(p => p.Parallel == parallel && p.Letter == letter);
    }

    public async Task<List<Form>> GetFormsByParallelAsync(int num, int pageNumber, int pageSize)
    {
        return _context.Forms.AsNoTracking().Where((p => p.Parallel == num))
            .ToPagedCollection(pageNumber, pageSize).ToList();
    }

    public async Task DeleteFormAsync(Form form)
    {
        if (form is not null)
        {
            _context.Forms.Remove(form);
            await _context.SaveChangesAsync();
        }    
    }

    public async Task EditFormAsync(Form form)
    {
        if (form is not null)
        {
            _context.Forms.Update(form);
            await _context.SaveChangesAsync();
        }    
    }

    public async Task AddFormAsync(Form form)
    {
        _context.Forms.Add(form);
        await _context.SaveChangesAsync();
    }
}
