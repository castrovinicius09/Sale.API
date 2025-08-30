using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library.
    /// This class centralizes all test data generation to ensure consistency
    /// across test cases and provide both valid and invalid data scenarios.
    /// </summary>
    internal static class SaleTestData
    {
        private static readonly Faker _faker = new Faker();

        /// <summary>
        /// Configures the Faker to generate valid Sale entities.
        /// The generated sales will have valid:
        /// - saleNumber (using internet usernames)
        /// - userId (meeting complexity requirements)
        /// - userName (valid format)
        /// - branchId (Brazilian format)
        /// - branchName (Active or Suspended)
        /// - branchAddress (Customer or Admin)
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
        /// Generates a branchName that exceeds the maximum length limit.
        /// The generated branchName will:
        /// - Be longer than 100 characters
        /// - Contain random alphanumeric characters
        /// This is useful for testing branchName length validation error cases.
        /// </summary>
        /// <returns>A branchName that exceeds the maximum length limit.</returns>
        public static string GenerateLongBranchName()
        {
            return new Faker().Random.String2(101);
        }

        /// <summary>
        /// Generates a branchAddredd that exceeds the maximum length limit.
        /// The generated branchAddredd will:
        /// - Be longer than 150 characters
        /// - Contain random alphanumeric characters
        /// This is useful for testing branchAddredd length validation error cases.
        /// </summary>
        /// <returns>A branchAddredd that exceeds the maximum length limit.</returns>
        public static string GenerateLongBranchAddress()
        {
            return new Faker().Random.String2(151);
        }
    }
}
