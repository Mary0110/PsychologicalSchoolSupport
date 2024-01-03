using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PsychologicalSupportPlatform.Common.Repository.Specifications;

namespace PsychologicalSupportPlatform.Common.Repository;

public abstract class SQLRepository<TDbContext, TEntity> : ISQLRepository<TEntity>
    where TEntity : class, new()
    where TDbContext : DbContext
{
    protected readonly TDbContext DbContext;
    private readonly DbSet<TEntity> _table;

    protected SQLRepository(TDbContext dbContext)
    {
        DbContext = dbContext;
        _table = dbContext.Set<TEntity>();
    }

    public virtual async Task<List<TEntity>> GetAllAsync(int pageNumber, int pageSize)
    {
        return DbContext
            .Set<TEntity>()
            .AsNoTracking()
            .ToPagedCollection(pageNumber, pageSize)
            .ToList();
    }

    public virtual async Task<List<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> predicate,
        int pageNumber,
        int pageSize
    )
    {
        return _table
            .AsNoTracking()
            .Where(predicate)
            .ToPagedCollection(pageNumber, pageSize)
            .ToList();
    }
    
    public virtual async Task<List<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> predicate
    )
    {
        return _table
            .AsNoTracking()
            .Where(predicate)
            .ToList();
    }

    public virtual async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _table.FindAsync(id);
    }

    public virtual async Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>> predicate
    )
    {
        return await _table.AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public async Task<TEntity> AddAsync(
        TEntity entity)
    {
        return (await DbContext.AddAsync(entity)).Entity;
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        DbContext.Entry(entity).State = EntityState.Modified;
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {    
        if (DbContext.Entry(entity).State == EntityState.Detached)
        {
            _table.Attach(entity);
        }
        
        _table.Remove(entity);
    }

    public virtual async Task<TEntity?> DeleteByIdAsync(int id)
    {
        var entity = await GetByIdAsync(id);

        if (entity != null)
        {
            DeleteAsync(entity);
        }

        return entity;
    }

    public virtual async Task SaveAsync()
    {
        await DbContext.SaveChangesAsync();
    }
    
    public async Task<List<TEntity>> GetBySpecificationAsync(
        Specification<TEntity> specification, int pageNumber, int pageSize
        )
    {
        var query = SpecificationQueryBuilder.GetQuery(_table.AsQueryable(), specification);
        var pagedQuery = query.ToPagedCollection(pageNumber, pageSize);

        return pagedQuery.ToList();
    }
    
    public async Task<List<TEntity>> GetAllBySpecificationAsync(
        Specification<TEntity> specification
    )
    {
        var query = SpecificationQueryBuilder.GetQuery(_table.AsQueryable(), specification);

        return query.ToList();
    }
}
