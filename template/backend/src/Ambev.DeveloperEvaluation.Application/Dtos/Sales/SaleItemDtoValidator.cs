using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Dtos.Sales
{
    /// <summary>
    /// Validator for SaleItemDto that defines validation rules for sale item creation.
    /// </summary>
    public sealed class SaleItemDtoValidator : AbstractValidator<SaleItemDto>
    {
        /// <summary>
        /// Initializes a new instance of the SaleItemDtoValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Quantity: Must be greater than zero
        /// - UnitPrice: Must be greater than zero
        /// - ProductId: Must be provided
        /// - ProductName: Required, must not exceed 100 characters
        /// </remarks>
        public SaleItemDtoValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("UnitPrice must be greater than zero.");

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId must be provided.");

            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("ProductName is required.")
                .MaximumLength(100).WithMessage("ProductName must not exceed 100 characters.");
        }
    }
}
