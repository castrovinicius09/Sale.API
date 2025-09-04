using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Bogus;

namespace Ambev.DeveloperEvaluation.Support.Domain.TestData
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library.
    /// This class centralizes all test data generation to ensure consistency
    /// across test cases and provide both valid and invalid data scenarios.
    /// </summary>
    public static class SaleTestData
    {
        private static readonly Faker _faker = new Faker();

        /// <summary>
        /// Configures the Faker to generate valid Sale entities.
        /// The generated sales will have valid:
        /// - saleNumber (using random number)
        /// - userId (using random Guid)
        /// - userName (usind user Faker test data)
        /// - branchId (using random Guid)
        /// - branchName (using company names)
        /// - branchAddress (using address)
        /// </summary>
        public static Sale GenerateValidSale()
        {
            var saleNumber = _faker.Random.Long(1, 10000);
            var userId = _faker.Random.Guid();
            var userName = UserTestData.GenerateValidUsername();
            var branchId = _faker.Random.Guid();
            var branchName = _faker.Company.CompanyName();
            var branchAddress = _faker.Address.FullAddress();

            // Chama o método estático de fábrica
            return Sale.Create(
                saleNumber,
                userId,
                userName,
                branchId,
                branchName,
                branchAddress);
        }

        /// <summary>
        /// Configures the Faker to generate invalid Sale entities.
        /// The generated sales will have valid:
        /// - saleNumber (using random number)
        /// - userId (empty Guid)
        /// - userName (usind user Faker test data)
        /// - branchId (empty Guid)
        /// - branchName (invalid name length)
        /// - branchAddress (invalid address name)
        /// </summary>
        public static Sale GenerateInvalidSale()
        {
            var saleNumber = 0;
            var userId = Guid.Empty;
            var userName = UserTestData.GenerateValidUsername();
            var branchId = Guid.Empty;
            var branchName = _faker.Random.String2(101);
            var branchAddress = _faker.Random.String2(151);

            // Chama o método estático de fábrica
            return Sale.Create(
                saleNumber,
                userId,
                userName,
                branchId,
                branchName,
                branchAddress);
        }
    }
}
