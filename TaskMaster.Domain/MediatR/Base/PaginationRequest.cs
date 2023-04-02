using FluentValidation;
using static TaskMaster.Shared.Constants;

namespace TaskMaster.Application.MediatR.Base
{
    public record PaginationRequest(int PageNumber = Pagination.DefaultPageNumber, int PageSize = Pagination.DefaultPageSize);

    public class PaginationRequestValidator : AbstractValidator<PaginationRequest>
    {
        public PaginationRequestValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(Pagination.DefaultPageNumber).WithMessage($"Page number must be at least {Pagination.DefaultPageNumber}.");
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(Pagination.DefaultPageSize).WithMessage($"Page size must be at least {Pagination.DefaultPageSize}.");
        }
    }
}
