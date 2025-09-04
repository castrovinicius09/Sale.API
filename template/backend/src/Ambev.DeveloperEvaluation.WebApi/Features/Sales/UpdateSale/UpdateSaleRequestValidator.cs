using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Shared.SaleItem;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// Validator for UpdateSaleRequest that defines validation rules for sale update fields.
    /// </summary>
    public sealed class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSaleValidator"/> with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Id: Must be provided and not empty
        /// - UserId: Must be provided and not empty
        /// - UserName: Required, must not exceed 100 characters
        /// - BranchId: Must be provided and not empty
        /// - BranchName: Required, must not exceed 100 characters
        /// - BranchFullAddress: Required, must not exceed 200 characters
        /// The Items property is ignored in this validator.
        /// </remarks>
        public UpdateSaleRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Sale Id must be provided.");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId must be provided.");

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
                .MaximumLength(200).WithMessage("BranchFullAddress must not exceed 200 characters.");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("A venda deve conter ao menos um item.")
                .ForEach(item =>
                {
                    item.SetValidator(new SaleItemRequestValidator());
                });
        }
    }
}
