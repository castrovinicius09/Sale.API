using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSales
{
    /// <summary>
    /// Validator for GetPaginatedSaleQuery
    /// </summary>
    public sealed class GetPaginatedSaleValidator : AbstractValidator<GetPaginatedSaleQuery>
    {
        /// <summary>
        /// Initializes validation rules for GetPaginatedSaleQuery
        /// </summary>
        public GetPaginatedSaleValidator()
        {
            RuleFor(x => x.PageNumber).NotEmpty().WithMessage("Page number is required");
            RuleFor(x => x.PageSize).NotEmpty().WithMessage("Page size is required");
        }
    }
}
