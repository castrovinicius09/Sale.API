using Ambev.DeveloperEvaluation.Application.Dtos.Sales;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById
{
    /// <summary>
    /// Profile for mapping between Sale entity list and GetPaginatedSale
    /// </summary>
    public class GetSaleByIdProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for GetPaginatedSale operation
        /// </summary>
        public GetSaleByIdProfile()
        {
            CreateMap<SaleItem, SaleItemDto>();
            CreateMap<Sale, GetSaleByIdResult>();
        }
    }
}
