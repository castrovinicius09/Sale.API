using Ambev.DeveloperEvaluation.Application.Services.Abstractions;

namespace Ambev.DeveloperEvaluation.Application.Services
{
    public class ProductService : IProductService
    {
        public void ValidateProduct(List<Guid> productIdList)
        {
            if (false)
                throw new NotImplementedException();
        }
    }
}
