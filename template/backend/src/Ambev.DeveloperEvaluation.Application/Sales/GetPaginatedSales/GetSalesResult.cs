namespace Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSales
{
    /// <summary>
    /// Response class for list of sales
    /// </summary>
    public record GetSalesResult
    {
        /// <summary>
        /// The unique identifier of the sale
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The unique sale number
        /// </summary>
        public long SaleNumber { get; }

        /// <summary>
        /// The user's name
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// The branch's name
        /// </summary>
        public string BranchName { get; }

        /// <summary>
        /// The branch's full address
        /// </summary>
        public string BranchFullAddress { get; }

        /// <summary>
        /// Number os the itens included in the sale
        /// </summary>
        public int TotalItens { get; }

        /// <summary>
        /// Total amount of the sale
        /// </summary>
        public decimal TotalSaleAmount { get; }

        /// <summary>
        /// Sale cancelled or not
        /// </summary>
        public bool Cancelled { get; }

        /// <summary>
        /// Sale created date
        /// </summary>
        public DateTime CreatedAt { get; }

        /// <summary>
        /// Sale updated date
        /// </summary>
        public DateTime? UpdatedAt { get; }

        /// <summary>
        /// Sale cancelled date
        /// </summary>
        public DateTime? CancelledAt { get; }
    }
}
