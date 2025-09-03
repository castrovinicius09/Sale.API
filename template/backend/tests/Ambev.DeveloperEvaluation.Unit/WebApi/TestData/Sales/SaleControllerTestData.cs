using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Support.Application;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.TestData.Sales
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library.
    /// This class centralizes all test data generation to ensure consistency
    /// across test cases and provide both valid and invalid data scenarios.
    /// </summary>
    public static class SaleControllerTestData
    {
        /// <summary>
        /// Configures the Faker to generate valid sale response objects with:
        /// - Id: Random GUID
        /// - SaleNumber: Random long
        /// - UserName: Internet-style username
        /// - BranchName: Company name
        /// - BranchFullAddress: Full street address
        /// - TotalItems: Between 1 and 10
        /// - TotalSaleAmount: Between 100 and 5000
        /// - Cancelled: Random boolean
        /// - CreatedAt: Recent date
        /// - UpdatedAt: Optional recent date
        /// - CancelledAt: Optional recent date if cancelled
        /// - SaleItems: List of valid sale items via <see cref="SaleItemApplicationTestData"/>
        /// </summary>
        private static readonly Faker<GetSaleByIdResult> GetByIdResultFaker = new Faker<GetSaleByIdResult>()
            .RuleFor(s => s.Id, f => f.Random.Guid())
            .RuleFor(s => s.SaleNumber, f => f.Random.Long(1000, 9999))
            .RuleFor(s => s.UserName, f => f.Internet.UserName())
            .RuleFor(s => s.BranchName, f => f.Company.CompanyName())
            .RuleFor(s => s.BranchFullAddress, f => f.Address.FullAddress())
            .RuleFor(s => s.TotalItems, f => f.Random.Int(1, 10))
            .RuleFor(s => s.TotalSaleAmount, f => f.Finance.Amount(100, 5000))
            .RuleFor(s => s.Cancelled, f => f.Random.Bool())
            .RuleFor(s => s.CreatedAt, f => f.Date.Past(1))
            .RuleFor(s => s.UpdatedAt, f => f.Random.Bool() ? f.Date.Recent(5) : null)
            .RuleFor(s => s.CancelledAt, (f, s) => s.Cancelled ? f.Date.Recent(10) : null)
            .RuleFor(s => s.SaleItems, f => SaleItemApplicationTestData.GenerateValidItems(f.Random.Int(1, 5)));

        /// <summary>
        /// Generates a valid <see cref="GetSaleByIdResult"/> with randomized data.
        /// </summary>
        /// <returns>A valid sale result that meets all validation requirements.</returns>
        public static GetSaleByIdResult GenerateValidGetSaleByIdResult()
        {
            return GetByIdResultFaker.Generate();
        }
    }
}
