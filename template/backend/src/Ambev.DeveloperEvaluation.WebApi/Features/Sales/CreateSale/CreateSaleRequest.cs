using Ambev.DeveloperEvaluation.Application.Dtos.Sales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Represents a request to create a new sale in the system.
    /// </summary>
    public class CreateSaleRequest
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
