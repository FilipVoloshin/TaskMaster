using static TaskMaster.Shared.Constants;

namespace TaskMaster.Application.MediatR.Base
{
    /// <summary>
    /// Represents a pagination request with a specified page number and page size.
    /// </summary>
    public record PaginationRequest
    {
        /// <summary>
        /// Gets or sets the page number to retrieve.
        /// </summary>
        public int PageNumber { get; init; } = Pagination.DefaultPageNumber;

        /// <summary>
        /// Gets or sets the number of items to retrieve per page.
        /// </summary>
        public int PageSize { get; init; } = Pagination.DefaultPageSize;
    }
}
