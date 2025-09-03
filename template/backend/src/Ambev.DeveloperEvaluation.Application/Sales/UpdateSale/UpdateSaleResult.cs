using Ambev.DeveloperEvaluation.Application.Dtos.Sales;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Result returned after updating an existing sale.
    /// </summary>
    public class UpdateSaleResult
    {
        /// <summary>
        /// Gets the unique identifier of the sale.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the sequential number assigned to the sale.
        /// </summary>
        public long SaleNumber { get; set; }

        /// <summary>
        /// Indicates whether the sale has been cancelled.
        /// </summary>
        public bool Cancelled { get; set; }

        /// <summary>
        /// Gets the date and time when the sale was originally created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets the date and time when the sale was last updated, if applicable.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets the date and time when the sale was cancelled, if applicable.
        /// </summary>
        public DateTime? CancelledAt { get; set; }

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
        /// Gets the total number of items included in the sale.
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Gets the total monetary value of the sale.
        /// </summary>
        public decimal TotalSaleAmount { get; set; }

        /// <summary>
        /// Gets the list of items included in the sale.
        /// </summary>
        public List<SaleItemDto> Items { get; set; } = new();
    }
}
