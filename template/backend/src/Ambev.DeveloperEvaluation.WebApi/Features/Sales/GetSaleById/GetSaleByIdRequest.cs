namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById
{
    /// <summary>
    /// Request model for getting a sale by ID
    /// </summary>
    public class GetSaleByIdRequest
    {
        /// <summary>
        /// The unique identifier of the sale to retrieve
        /// </summary>
        public Guid Id { get; set; }
    }
}
