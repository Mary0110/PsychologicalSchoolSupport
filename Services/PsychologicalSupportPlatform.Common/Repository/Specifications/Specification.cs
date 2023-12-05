using System.Linq.Expressions;

namespace PsychologicalSupportPlatform.Common.Repository.Specifications;

public abstract class Specification<TEntity>
    where TEntity : class, new()
{
    public Specification()
    {
    }

    public Specification (Expression<Func<TEntity, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<TEntity, bool>>? Criteria { get; }
}
