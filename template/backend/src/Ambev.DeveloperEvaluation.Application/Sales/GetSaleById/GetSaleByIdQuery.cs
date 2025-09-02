using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById
{
    /// <summary>
    /// Query for retrieving a paginated list of sales
    /// </summary>
    public record GetSaleByIdQuery : IRequest<GetSaleByIdResult>
    {
        public GetSaleByIdQuery(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// The unique identifier of the sale to retrieve
        /// </summary>
        public Guid Id { get; }
    }
}
