using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Sales
{
    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleFor(user => user.SaleNumber)
                .GreaterThan(0).WithMessage("Sale number must be greater than zero.");

            RuleFor(user => user.CreateAt)
               .NotNull().WithMessage("Sale create date is required.");

            RuleFor(user => user.UserId)
               .NotNull().WithMessage("UserId is required.");

            RuleFor(user => user.UserName)
               .NotNull().WithMessage("Username is required.")
               .MaximumLength(50).WithMessage("Username cannot be longer than 50 characters.");

            RuleFor(user => user.BranchId)
               .NotNull().WithMessage("BranchId is required.");

            RuleFor(user => user.BranchName)
               .NotNull().WithMessage("BranchName is required.")
               .NotEmpty().WithMessage("Branch name cannot be empty.")
               .MaximumLength(100).WithMessage("Branch name cannot be longer than 100 characters.");

            RuleFor(user => user.BranchFullAddress)
               .NotNull().WithMessage("Username is required.")
               .NotEmpty().WithMessage("Branch address cannot be empty.")
               .MaximumLength(150).WithMessage("Branch address cannot be longer than 150 characters.");
        }
    }
}
