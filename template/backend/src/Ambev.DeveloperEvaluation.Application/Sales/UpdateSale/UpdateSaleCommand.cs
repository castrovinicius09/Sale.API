using Ambev.DeveloperEvaluation.Application.Dtos.Sales;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
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
