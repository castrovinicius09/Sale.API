using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSales
{
    /// <summary>
    /// Query for retrieving a paginated list of sales
    /// </summary>
    public record GetPaginatedSaleQuery : IRequest<IEnumerable<GetPaginatedSalesResult>>
    {
        public GetPaginatedSaleQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        /// <summary>
        /// The number of the page to retrieve
        /// </summary>
        public int PageNumber { get; }
        /// <summary>
        /// The number os the elements per page
        /// </summary>
        public int PageSize { get; }
    }
}
