using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSales
{
    /// <summary>
    /// Profile for mapping between Sale entity list and GetPaginatedSale
    /// </summary>
    public class GetPaginatedSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for GetPaginatedSale operation
        /// </summary>
        public GetPaginatedSaleProfile()
        {
            CreateMap<Sale, GetPaginatedSalesResult>();
        }
    }
}
