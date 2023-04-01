using Ardalis.Specification;
using TaskMaster.Infrastructure.Entities.Abstractions;
using TaskMaster.Infrastructure.Entities.Base;

namespace TaskMaster.Infrastructure.Specifications.Core
{
    public class AuthorFilterSpecification<T> : Specification<T>
        where T : BaseEntity, IAuthorable
    {
        public AuthorFilterSpecification(Guid userId)
        {
            Query.Where(entity => entity.AuthorId == userId);
        }
    }
}
