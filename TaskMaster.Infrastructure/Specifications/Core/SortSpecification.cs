using Ardalis.Specification;
using System.Linq.Expressions;
using TaskMaster.Infrastructure.Entities.Base;

namespace TaskMaster.Infrastructure.Specifications.Core
{
    public class SortSpecification<T> : Specification<T> where T : BaseEntity
    {
        public SortSpecification(Expression<Func<T, object?>> sortExpression, bool descending = false)
        {
            if (descending)
            {
                Query.OrderByDescending(sortExpression);
            }
            else
            {
                Query.OrderBy(sortExpression);
            }
        }
    }
}
