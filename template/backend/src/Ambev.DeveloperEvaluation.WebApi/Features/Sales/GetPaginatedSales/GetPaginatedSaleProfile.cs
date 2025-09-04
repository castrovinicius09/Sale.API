using Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetPaginatedSales
{
    public sealed class GetPaginatedSaleProfile : Profile
    {
        public GetPaginatedSaleProfile()
        {
            CreateMap<GetPaginatedSalesRequest, GetPaginatedSalesQuery>();
            CreateMap<GetPaginatedSalesResult, GetPaginatedSalesResponse>();
        }
    }
}
