using Ambev.DeveloperEvaluation.Application.Dtos.Sales;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Validator for CreateSaleCommand that defines validation rules for sale creation command.
    /// </summary>
    public sealed class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the CreateSaleCommandValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - UserId: Must be provided and not empty
        /// - UserName: Required, must not exceed 100 characters
        /// - BranchId: Must be provided and not empty
        /// - BranchName: Required, must not exceed 100 characters
        /// - BranchFullAddress: Required, must not exceed 200 characters
        /// - Items: Must contain at least one valid item
        /// 
        /// Each item in the sale is validated using <see cref="SaleItemDtoValidator"/>, which enforces:
        /// </remarks>
        public CreateSaleValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId must be provided.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required.")
                .MaximumLength(100).WithMessage("UserName must not exceed 100 characters.");

            RuleFor(x => x.BranchId)
                .NotEmpty().WithMessage("BranchId must be provided.");

            RuleFor(x => x.BranchName)
                .NotEmpty().WithMessage("BranchName is required.")
                .MaximumLength(100).WithMessage("BranchName must not exceed 100 characters.");

            RuleFor(x => x.BranchFullAddress)
                .NotEmpty().WithMessage("BranchFullAddress is required.")
                .MaximumLength(150).WithMessage("BranchFullAddress must not exceed 200 characters.");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("At least one sale item must be provided.")
                .Must(items => items.All(item => item != null)).WithMessage("Sale items cannot contain null entries.");

            RuleForEach(x => x.Items).SetValidator(new SaleItemDtoValidator());
        }
    }
}
