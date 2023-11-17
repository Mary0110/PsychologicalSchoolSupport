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
        return DbContext
            .Set<TEntity>()
            .AsNoTracking()
            .Where(predicate)
            .ToPagedCollection(pageNumber, pageSize)
            .ToList();
    }
    
    public virtual async Task<List<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> predicate
    )
    {
        return DbContext
            .Set<TEntity>()
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
        return await _table.FirstOrDefaultAsync(predicate);
    }

    public async Task<TEntity> AddAsync(
        TEntity entity)
    {
        return (await DbContext.AddAsync(entity)).Entity;
    }

    public virtual void Update(TEntity entity)
    {
        DbContext.Entry(entity).State = EntityState.Modified;
    }

    public virtual void Delete(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);
    }

    public virtual async Task<TEntity?> DeleteByIdAsync(int id)
    {
        var entity = await GetByIdAsync(id);

        if (entity != null)
        {
            Delete(entity);
        }

        return entity;
    }

    public virtual async Task SaveAsync()
    {
        await DbContext.SaveChangesAsync();
    }
    
    public async Task<List<TEntity>> GetBySpecificationAsync(Specification<TEntity> specification, int pageNumber, int pageSize)
    {
        var query = SpecificationQueryBuilder.GetQuery(DbContext.Set<TEntity>().AsQueryable(), specification);
        var pagedQuery = query.ToPagedCollection(pageNumber, pageSize);

        return pagedQuery.ToList();
    }
}
