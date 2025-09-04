using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Profile for mapping between Sale entity and Request/Response
    /// </summary>
    public sealed class CreateSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateSale operation
        /// </summary>
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleCommand, Sale>().ConstructUsing(dto =>
                Sale.Create(
                    dto.SaleNumber,
                    dto.UserId,
                    dto.UserName,
                    dto.BranchId,
                    dto.BranchName,
                    dto.BranchFullAddress))
            .AfterMap((dto, sale, ctx) =>
            {
                foreach (var itemDto in dto.Items)
                {
                    sale.AddItem(
                        itemDto.Quantity,
                        itemDto.UnitPrice,
                        itemDto.ProductId,
                        itemDto.ProductName);
                }
            });

            CreateMap<Sale, CreateSaleResult>();
        }
    }
}
