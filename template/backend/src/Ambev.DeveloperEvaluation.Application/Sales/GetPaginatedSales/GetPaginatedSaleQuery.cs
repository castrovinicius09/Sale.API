using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSales
{
    /// <summary>
    /// Query for retrieving a paginated list of sales
    /// </summary>
    public record GetPaginatedSaleQuery : IRequest<GetSalesResult>
    {
        /// <summary>
        /// The unique identifier of the user to retrieve
        /// </summary>
        public int PageNumber { get; }
        /// <summary>
        /// The unique identifier of the user to retrieve
        /// </summary>
        public int PageSize { get; }
    }
}
