using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library.
    /// This class centralizes all test data generation to ensure consistency
    /// across test cases and provide both valid and invalid data scenarios.
    /// </summary>
    internal static class SaleItemTestData
    {
        private static readonly Faker _faker = new Faker();

        /// <summary>
        /// Configures the Faker to generate valid Sale item entity.
        /// The generated sales will have valid:
        /// - quantity (using fixed value)
        /// - unitPrice (using random decimal to price)
        /// - productId (using random Guid)
        /// - productName (using random commerce product name)
        /// </summary>
        public static SaleItem GenerateValidSaleItem()
        {
            var quantity = 10;
            decimal unitPrice = Math.Round(_faker.Random.Decimal(2, 1000), 2);
            var productId = _faker.Random.Guid();
            var productName = _faker.Commerce.ProductName();

            // Chama o método estático de fábrica
            return SaleItem.Create(
                quantity,
                unitPrice,
                productId,
                productName);
        }

        /// <summary>
        /// Configures the Faker to generate invalid Sale item entity.
        /// The generated sales will have valid:
        /// - quantity (invalid number)
        /// - unitPrice (invalid number)
        /// - productId (empty Guid)
        /// - productName (invlaid string length)
        /// </summary>
        public static SaleItem GenerateInvalidSaleItem()
        {
            var quantity = 0;
            var unitPrice = 0;
            var productId = Guid.Empty;
            var productName = _faker.Random.String2(101);

            // Chama o método estático de fábrica
            return SaleItem.Create(
                quantity,
                unitPrice,
                productId,
                productName);
        }
    }
}
