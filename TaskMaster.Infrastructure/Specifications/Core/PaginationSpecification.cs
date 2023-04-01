using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.Infrastructure.Entities.Base;

namespace TaskMaster.Infrastructure.Specifications.Core
{
    public class PaginationSpecification<T> : Specification<T>
        where T : BaseEntity
    {
        protected PaginationSpecification(int page, int pageSize)
        {
            Query.Skip((page - 1) * pageSize)
             .Take(pageSize);
        }
    }
}
