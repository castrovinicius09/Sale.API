using Ambev.DeveloperEvaluation.Application.Dtos.Sales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById
{
    /// <summary>
    /// Response class for a sale
    /// </summary>
    public class GetSaleByIdResponse
    {
        /// <summary>
        /// The unique identifier of the sale
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The unique sale number
        /// </summary>
        public long SaleNumber { get; set; }

        /// <summary>
        /// The user's name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The branch's name
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// The branch's full address
        /// </summary>
        public string BranchFullAddress { get; set; }

        /// <summary>
        /// Number os the items included in the sale
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Total amount of the sale
        /// </summary>
        public decimal TotalSaleAmount { get; set; }

        /// <summary>
        /// Sale cancelled or not
        /// </summary>
        public bool Cancelled { get; set; }

        /// <summary>
        /// Sale created date
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Sale updated date
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Sale cancelled date
        /// </summary>
        public DateTime? CancelledAt { get; set; }

        /// <summary>
        /// Sale item list
        /// </summary>
        public IReadOnlyList<SaleItemDto> SaleItems { get; set; }
    }
}
