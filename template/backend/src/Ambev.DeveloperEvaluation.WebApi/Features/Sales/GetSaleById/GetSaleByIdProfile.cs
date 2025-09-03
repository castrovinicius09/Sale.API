using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById
{
    /// <summary>
    /// Profile for mapping GetSaleById feature requests to commands
    /// </summary>
    public sealed class GetSaleByIdProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for GetUser feature
        /// </summary>
        public GetSaleByIdProfile()
        {
            CreateMap<GetSaleByIdRequest, GetSaleByIdQuery>();
            CreateMap<GetSaleByIdResult, GetSaleByIdResponse>();
        }
    }
}
