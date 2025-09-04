using Ambev.DeveloperEvaluation.Application.Dtos.Sales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Shared.SaleItem
{
    public class SaleItemProfile : Profile
    {
        public SaleItemProfile()
        {
            CreateMap<SaleItemRequest, SaleItemDto>();
        }
    }
}
