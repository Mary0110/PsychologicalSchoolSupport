using System.Linq.Expressions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using PsychologicalSupportPlatform.Edu.Application.Interfaces;
using PsychologicalSupportPlatform.Edu.Domain.Entities;

namespace PsychologicalSupportPlatform.Edu.Infrastructure.Data.Repositories;

public class CachedEduMaterialRepository:IEduMaterialRepository
{
    private readonly EduMaterialRepository _decorated;
    
    private readonly IDistributedCache _distributedCache;

    public CachedEduMaterialRepository(EduMaterialRepository decorated, IDistributedCache distributedCache)
    {
        _decorated = decorated;
        _distributedCache = distributedCache;
    }

    public Task<List<EduMaterial>> GetAllAsync(int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<List<EduMaterial>> GetAllAsync(Expression<Func<EduMaterial, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<List<EduMaterial>> GetAllAsync(Expression<Func<EduMaterial, bool>> predicate, int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<EduMaterial?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<EduMaterial?> GetAsync(Expression<Func<EduMaterial, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<EduMaterial> AddAsync(EduMaterial entity)
    {
        throw new NotImplementedException();
    }

    public Task AddRangeAsync(List<EduMaterial> entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(EduMaterial entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(EduMaterial entity)
    {
        throw new NotImplementedException();
    }

    public Task<EduMaterial?> DeleteByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}