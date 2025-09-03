using Ambev.DeveloperEvaluation.Application.Sales.CancellSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancellSale
{
    /// <summary>
    /// Validator for CancellSaleRequest that defines validation rules for sale fields
    /// </summary>
    public class CancellSaleRequestValidator : AbstractValidator<CancellSaleRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CancellSaleValidator"/> with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Id: Must be provided and not empty
        /// </remarks>
        public CancellSaleRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Sale Id must be provided.");
        }
    }
}
