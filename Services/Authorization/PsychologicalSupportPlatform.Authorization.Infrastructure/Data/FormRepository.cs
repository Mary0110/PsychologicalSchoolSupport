using PsychologicalSupportPlatform.Authorization.Application.Interfaces;
using PsychologicalSupportPlatform.Authorization.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PsychologicalSupportPlatform.Authorization.Infrastructure.Data;

public class FormRepository : IFormRepository
{
    private readonly DataContext context;

    public FormRepository(DataContext context)
    {
        this.context = context;
    }
    
    public async Task<List<Form>> GetAllFormsAsync(int pageNumber, int pageSize)
    {
        return context.Forms.AsNoTracking().ToPagedCollection(pageNumber, pageSize).ToList();
    }

    public async Task<Form?> GetFormAsync(int parallel, char letter)
    {
        return await context.Forms.AsNoTracking().FirstOrDefaultAsync(p => p.Parallel == parallel && p.Letter == letter);
    }

    public async Task<List<Form>> GetFormsByParallelAsync(int num, int pageNumber, int pageSize)
    {
        return context.Forms.AsNoTracking().Where((p => p.Parallel == num))
            .ToPagedCollection(pageNumber, pageSize).ToList();
    }

    public async Task DeleteFormAsync(Form form)
    {
        if (form is not null)
        {
            context.Forms.Remove(form);
            await context.SaveChangesAsync();
        }    
    }

    public async Task EditFormAsync(Form form)
    {
        if (form is not null)
        {
            context.Forms.Update(form);
            await context.SaveChangesAsync();
        }    
    }

    public async Task AddFormAsync(Form form)
    {
        context.Forms.Add(form);
        await context.SaveChangesAsync();
    }
}