using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSales
{
    /// <summary>
    /// Validator for GetPaginatedSaleQuery
    /// </summary>
    public sealed class GetPaginatedSaleValidator : AbstractValidator<GetPaginatedSalesQuery>
    {
        /// <summary>
        /// Initializes validation rules for GetPaginatedSaleQuery
        /// </summary>
        public GetPaginatedSaleValidator()
        {
            RuleFor(x => x.PageNumber)
                .NotEmpty().WithMessage("Page number is required")
                .GreaterThanOrEqualTo(1).WithMessage("Page number can´t be negative");

            RuleFor(x => x.PageSize)
                .NotEmpty().WithMessage("Page size is required")
                .GreaterThanOrEqualTo(1).WithMessage("Page size can´t be negative or zero");
        }
    }
}
