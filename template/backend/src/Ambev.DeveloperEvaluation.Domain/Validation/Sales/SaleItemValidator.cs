using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Sales
{
    public sealed class SaleItemValidator : AbstractValidator<SaleItem>
    {
        public SaleItemValidator()
        {
            RuleFor(user => user.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.")
                .LessThanOrEqualTo(20).WithMessage("Quantity must be less than zero or equal 20.");

            RuleFor(user => user.UnitPrice)
                .GreaterThan(0).WithMessage("Unit Price must be greater than zero.");

            RuleFor(user => user.TotalAmount)
                .GreaterThan(0).WithMessage("Total AMount must be greater than zero.");

            RuleFor(user => user.ProductId)
               .NotEmpty().WithMessage("ProductId cannot be empty.");

            RuleFor(user => user.ProductName)
               .NotEmpty().WithMessage("Product name cannot be empty.")
               .MaximumLength(100).WithMessage("Product name cannot be longer than 100 characters.");
        }
    }
}
