using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById
{
    /// <summary>
    /// Validator for GetSaleByIdRequest
    /// </summary>
    public class GetSaleByIdRequestValidator : AbstractValidator<GetSaleByIdRequest>
    {
        /// <summary>
        /// Initializes validation rules for GetSaleByIdRequest
        /// </summary>
        public GetSaleByIdRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Sale ID is required");
        }
    }
}
