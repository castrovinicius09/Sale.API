namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetPaginatedSales
{
    /// <summary>
    /// Request model for getting a paginated sale list by pageNumber and pageSize
    /// </summary>
    public class GetPaginatedSalesRequest
    {
        /// <summary>
        /// The number of the actual page
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// the number os a elements that will be returned in the list
        /// </summary>
        public int PageSize { get; set; }
    }
}
