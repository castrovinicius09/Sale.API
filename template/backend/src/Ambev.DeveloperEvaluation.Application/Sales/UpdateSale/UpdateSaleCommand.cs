using Ambev.DeveloperEvaluation.Application.Dtos.Sales;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Command for update a existing sale.
    /// </summary>
    /// <remarks>
    /// This command is used to capture the required data for update a sale,
    /// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
    /// that returns a <see cref="bool"/>.
    ///
    /// The data provided in this command is validated using the 
    /// <see cref="UpdateSaleValidator"/> which extends 
    /// <see cref="AbstractValidator{T}"/> to ensure that all fields are correctly 
    /// populated and follow the business rules for sale creation.
    /// </remarks>
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        /// <summary>
        /// Gets the unique identifier of the sale.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Indicates whether the sale has been cancelled.
        /// </summary>
        public bool Cancelled { get; set; }

        /// <summary>
        /// Gets the unique identifier of the user who made the sale.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets the name of the user who made the sale.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets the unique identifier of the branch where the sale occurred.
        /// </summary>
        public Guid BranchId { get; set; }

        /// <summary>
        /// Gets the name of the branch where the sale occurred.
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// Gets the full address of the branch where the sale occurred.
        /// </summary>
        public string BranchFullAddress { get; set; }

        /// <summary>
        /// Gets the list of items included in the sale.
        /// </summary>
        public List<SaleItemDto> Items { get; set; } = new();
    }
}
