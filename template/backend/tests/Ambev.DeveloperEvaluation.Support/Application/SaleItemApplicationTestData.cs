using Ambev.DeveloperEvaluation.Application.Dtos.Sales;
using Bogus;

namespace Ambev.DeveloperEvaluation.Support.Application
{
    /// <summary>
    /// Provides test data for <see cref="SaleItemDto"/> used in sale item creation.
    /// </summary>
    public static class SaleItemApplicationTestData
    {
        /// <summary>
        /// Configures the Faker to generate valid sale items with:
        /// - Quantity: Between 1 and 10
        /// - UnitPrice: Between 10.00 and 500.00
        /// - ProductId: Random GUID
        /// - ProductName: Commerce product name
        /// </summary>
        private static readonly Faker<SaleItemDto> saleItemFaker = new Faker<SaleItemDto>()
            .RuleFor(i => i.Quantity, f => f.Random.Int(1, 10))
            .RuleFor(i => i.UnitPrice, f => f.Finance.Amount(10, 500))
            .RuleFor(i => i.ProductId, f => f.Random.Guid())
            .RuleFor(i => i.ProductName, f => f.Commerce.ProductName());

        /// <summary>
        /// Generates a list of valid <see cref="SaleItemDto"/> entries.
        /// </summary>
        /// <param name="count">The number of items to generate.</param>
        /// <returns>A list of valid sale items.</returns>
        public static List<SaleItemDto> GenerateValidItems(int count)
        {
            return saleItemFaker.Generate(count);
        }

        public static List<SaleItemDto> GenerateInvalidItems()
        {
            var saleitem = saleItemFaker.Generate(1);
            saleitem[0].ProductId = Guid.Empty;

            return saleitem;
        }
    }
}
