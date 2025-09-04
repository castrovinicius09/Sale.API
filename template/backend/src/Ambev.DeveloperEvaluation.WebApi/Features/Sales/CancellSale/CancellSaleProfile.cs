using Ambev.DeveloperEvaluation.Application.Sales.CancellSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancellSale
{
    /// <summary>
    /// Profile for mapping between Application and API CancellSale request/response
    /// </summary>
    public class CancellSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CancellSale feature
        /// </summary>
        public CancellSaleProfile()
        {
            CreateMap<CancellSaleRequest, CancellSaleCommand>();
        }
    }
}
