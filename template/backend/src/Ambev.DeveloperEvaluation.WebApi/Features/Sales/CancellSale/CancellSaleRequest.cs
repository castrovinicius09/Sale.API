namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancellSale
{
    /// <summary>
    /// Represents a request to cancell a existing sale in the system.
    /// </summary>
    public class CancellSaleRequest
    {
        /// <summary>
        /// Gets the unique identifier of sale.
        /// </summary>
        public Guid Id { get; set; }
    }
}
