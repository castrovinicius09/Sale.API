using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Sales
{
    public sealed class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleFor(user => user.SaleNumber)
                .GreaterThan(0).WithMessage("Sale number must be greater than zero.");

            RuleFor(user => user.UserId)
               .NotEmpty().WithMessage("UserId cannot be empty.");

            RuleFor(user => user.UserName)
               .NotEmpty().WithMessage("Username cannot be empty.")
               .MaximumLength(50).WithMessage("Username cannot be longer than 50 characters.");

            RuleFor(user => user.BranchId)
               .NotEmpty().WithMessage("BranchId cannot be empty.");

            RuleFor(user => user.BranchName)
               .NotEmpty().WithMessage("Branch name cannot be empty.")
               .MaximumLength(100).WithMessage("Branch name cannot be longer than 100 characters.");

            RuleFor(user => user.BranchFullAddress)
               .NotEmpty().WithMessage("Branch address cannot be empty.")
               .MaximumLength(150).WithMessage("Branch address cannot be longer than 150 characters.");
        }
    }
}
