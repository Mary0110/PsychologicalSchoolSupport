using System.Linq.Expressions;

namespace PsychologicalSupportPlatform.Common.Repository;

public interface ISQLRepository<TEntity> where TEntity : class
{
    Task<List<TEntity>> GetAllAsync(int pageNumber, int pageSize);

    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

    Task<List<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> predicate,
        int pageNumber, 
        int pageSize
    );
    
    Task<TEntity?> GetByIdAsync(int id);
    
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);
    
    Task<TEntity> AddAsync(TEntity entity);
    
    void Update(TEntity entity);
    
    void Delete(TEntity entity);
    
    Task<TEntity?> DeleteByIdAsync(int id);
    
    Task SaveAsync();
}
