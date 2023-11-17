namespace PsychologicalSupportPlatform.Common.Repository.Specifications;

public static class SpecificationQueryBuilder
{
    public static IQueryable<TEntity> GetQuery<TEntity>(
        IQueryable<TEntity> inputQuery,
        Specification<TEntity> specification)
        where TEntity : class, new()
    {
        var query = inputQuery;
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }
        
        return query;
    }
}
