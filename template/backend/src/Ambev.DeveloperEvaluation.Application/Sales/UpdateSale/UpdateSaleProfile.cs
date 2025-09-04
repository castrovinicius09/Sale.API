using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Profile for mapping between Sale entity and Request/Response
    /// </summary>
    public sealed class UpdateSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for UpdateSale operation
        /// </summary>
        public UpdateSaleProfile()
        {
            //CreateMap<UpdateSaleCommand, Sale>()
            //    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            //    .ForMember(dest => dest.CancelledAt, opt => opt.MapFrom(src =>
            //        src.Cancelled ? DateTime.UtcNow : (DateTime?)null));

            CreateMap<UpdateSaleCommand, Sale>()
                .AfterMap((dto, sale) =>
                {
                    sale.Update(
                        dto.UserId,
                        dto.UserName,
                        dto.BranchId,
                        dto.BranchName,
                        dto.BranchFullAddress);

                    foreach (var itemDto in dto.Items)
                    {
                        sale.AddItem(
                            itemDto.Quantity,
                            itemDto.UnitPrice,
                            itemDto.ProductId,
                            itemDto.ProductName);
                    }
                });

            CreateMap<Sale, UpdateSaleResult>();
        }
    }
}
