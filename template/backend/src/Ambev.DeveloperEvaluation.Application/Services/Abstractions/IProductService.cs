
namespace Ambev.DeveloperEvaluation.Application.Services.Abstractions
{
    public interface IProductService
    {
        void ValidateProduct(IEnumerable<Guid> productIdList);
    }
}
