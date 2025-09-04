using Ambev.DeveloperEvaluation.Application.Dtos.Sales;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Command for creating a new sale.
    /// </summary>
    /// <remarks>
    /// This command is used to capture the required data for registering a sale, 
    /// including user information, branch details, and the list of sale items. 
    /// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
    /// that returns a <see cref="CreateSaleResult"/>.
    ///
    /// The data provided in this command is validated using the 
    /// <see cref="CreateSaleCommandValidator"/> which extends 
    /// <see cref="AbstractValidator{T}"/> to ensure that all fields are correctly 
    /// populated and follow the business rules for sale creation.
    /// </remarks>
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        /// <summary>
        /// the sale number previously created
        /// </summary>
        public long SaleNumber { get; set; }

        /// <summary>
        /// Gets the unique identifier of the user making the sale.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets the name of the user making the sale.
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Gets the unique identifier of the branch where the sale occurred.
        /// </summary>
        public Guid BranchId { get; set; }

        /// <summary>
        /// Gets the name of the branch where the sale occurred.
        /// </summary>
        public string BranchName { get; set; } = string.Empty;

        /// <summary>
        /// Gets the full address of the branch.
        /// </summary>
        public string BranchFullAddress { get; set; } = string.Empty;

        /// <summary>
        /// Gets the list of items included in the sale.
        /// </summary>
        public List<SaleItemDto> Items { get; set; } = new();
    }
}
