using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetPaginatedSales
{
    /// <summary>
    /// Validator for GetPaginatedSalesRequest
    /// </summary>
    public class GetPaginatedSalesRequestValidator : AbstractValidator<GetPaginatedSalesRequest>
    {
        public GetPaginatedSalesRequestValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(0).WithMessage("Page number can´t be negative");
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1).WithMessage("Page size can´t be negative or zero");
        }
    }
}
