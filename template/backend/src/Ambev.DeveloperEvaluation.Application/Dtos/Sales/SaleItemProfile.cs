using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Dtos.Sales
{
    /// <summary>
    /// Profile for mapping between Saleitem entity list and dto
    /// </summary>
    public sealed class SaleItemProfile : Profile
    {
        public SaleItemProfile()
        {
            CreateMap<SaleItem, SaleItemDto>().ReverseMap();
        }
    }
}
