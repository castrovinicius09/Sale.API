using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancellSale
{
    /// <summary>
    /// Validator for sale data that defines validation rules for core sale properties.
    /// </summary>
    public sealed class CancellSaleValidator : AbstractValidator<CancellSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CancellSaleValidator"/> with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Id: Must be provided and not empty
        /// 
        /// The Items property is ignored in this validator.
        /// </remarks>
        public CancellSaleValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Sale Id must be provided.");
        }
    }
}
