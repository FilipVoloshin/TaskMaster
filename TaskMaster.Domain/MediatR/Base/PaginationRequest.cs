using FluentValidation;
using static TaskMaster.Shared.Constants;

namespace TaskMaster.Application.MediatR.Base
{
    public class PaginationRequest
    {
        public int PageNumber { get; set; } = Pagination.DefaultPageNumber;
        public int PageSize { get; set; } = Pagination.DefaultPageSize;
    }
}
