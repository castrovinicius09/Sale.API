using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById
{
    /// <summary>
    /// Validator for GetSaleByIdQuery
    /// </summary>
    public sealed class GetSaleByIdValidator : AbstractValidator<GetSaleByIdQuery>
    {
        /// <summary>
        /// Initializes validation rules for GetSaleByIdQuery
        /// </summary>
        public GetSaleByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sale Id is required");
        }
    }
}
