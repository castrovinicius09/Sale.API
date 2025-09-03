using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancellSale
{
    /// <summary>
    /// Command for cancell a existing sale.
    /// </summary>
    /// <remarks>
    /// This command is used to capture the required data for cancell a sale,
    /// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
    /// that returns a <see cref="bool"/>.
    ///
    /// The data provided in this command is validated using the 
    /// <see cref="CancellSaleValidator"/> which extends 
    /// <see cref="AbstractValidator{T}"/> to ensure that all fields are correctly 
    /// populated and follow the business rules for sale creation.
    /// </remarks>
    public class CancellSaleCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets the unique identifier of the sale.
        /// </summary>
        public Guid Id { get; set; }
    }
}
