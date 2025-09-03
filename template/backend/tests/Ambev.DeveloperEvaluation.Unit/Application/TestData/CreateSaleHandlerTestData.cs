using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library.
    /// This class centralizes all test data generation to ensure consistency
    /// across test cases and provide both valid and invalid data scenarios.
    /// </summary>
    public static class CreateSaleHandlerTestData
    {
        /// <summary>
        /// Configures the Faker to generate valid sale commands with:
        /// - UserId: Random GUID
        /// - UserName: Internet-style username
        /// - BranchId: Random GUID
        /// - BranchName: Company name
        /// - BranchFullAddress: Full street address
        /// - Items: List of valid sale items generated via <see cref="SaleItemDtoFaker"/>
        /// </summary>
        private static readonly Faker<CreateSaleCommand> createSaleFaker = new Faker<CreateSaleCommand>()
            .RuleFor(c => c.UserId, f => f.Random.Guid())
            .RuleFor(c => c.UserName, f => f.Internet.UserName())
            .RuleFor(c => c.BranchId, f => f.Random.Guid())
            .RuleFor(c => c.BranchName, f => f.Company.CompanyName())
            .RuleFor(c => c.BranchFullAddress, f => f.Address.FullAddress())
            .RuleFor(c => c.Items, f => SaleItemTestData.GenerateValidItems(f.Random.Int(1, 5)));

        /// <summary>
        /// Generates a valid <see cref="CreateSaleCommand"/> with randomized data.
        /// </summary>
        /// <returns>A valid sale command that meets all validation requirements.</returns>
        public static CreateSaleCommand GenerateValidCommand()
        {
            return createSaleFaker.Generate();
        }

        /// <summary>
        /// Generates a valid <see cref="CreateSaleCommand"/> with randomized data.
        /// </summary>
        /// <returns>A invvalid sale command that meets all validation requirements.</returns>
        public static CreateSaleCommand GenerateInvalidCommand()
        {
            var sale = createSaleFaker.Generate();
            sale.UserName = string.Empty; // Invalid: Username is required
            sale.UserId = Guid.Empty; // Invalid: UserId is required
            sale.BranchId = Guid.Empty; // Invalid: BranchId is required
            sale.BranchName = string.Empty; // Invalid: BranchName is required
            sale.Items = SaleItemTestData.GenerateInvalidItems();

            return sale;
        }
    }
}
