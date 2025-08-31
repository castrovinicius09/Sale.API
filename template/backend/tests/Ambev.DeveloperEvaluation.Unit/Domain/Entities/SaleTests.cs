using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleTests
    {
        /// <summary>
        /// Tests that validation passes when a sale has valid data.
        /// </summary>
        [Fact(DisplayName = "Validation should pass for valid sale data")]
        public void Given_ValidSaleData_When_Validated_Then_ShouldReturnValid()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            // Act
            var result = sale.Validate();

            // Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        /// <summary>
        /// Tests that validation fails when a sale has invalid data.
        /// </summary>
        [Fact(DisplayName = "Validation should fail for invalid sale data")]
        public void Given_InvalidSaleData_When_Validated_Then_ShouldReturnInvalid()
        {
            // Arrange
            var sale = SaleTestData.GenerateInvalidSale();

            // Act
            var result = sale.Validate();

            // Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        /// <summary>
        /// Tests that when a sale is created, its properties are initialized correctly.
        /// </summary>
        [Fact(DisplayName = "Sale should be created with correct properties")]
        public void Given_ValidData_When_Created_Then_PropertiesShouldBeSet()
        {
            // Arrange
            var validSale = SaleTestData.GenerateValidSale();

            // Act
            var sale = Sale.Create(
                validSale.SaleNumber,
                validSale.UserId,
                validSale.UserName,
                validSale.BranchId,
                validSale.BranchName,
                validSale.BranchFullAddress);

            // Assert
            Assert.Equal(validSale.SaleNumber, sale.SaleNumber);
            Assert.Equal(validSale.UserId, sale.UserId);
            Assert.Equal(validSale.UserName, sale.UserName);
            Assert.Equal(validSale.BranchId, sale.BranchId);
            Assert.Equal(validSale.BranchName, sale.BranchName);
            Assert.Equal(validSale.BranchFullAddress, sale.BranchFullAddress);
            Assert.False(sale.Cancelled);
            Assert.Equal(validSale.Cancelled, sale.Cancelled);
        }

        /// <summary>
        /// Tests that when a sale is updated, its properties are changed accordingly.
        /// </summary>
        [Fact(DisplayName = "Sale should update properties when updated")]
        public void Given_ExistingSale_When_Updated_Then_PropertiesShouldChange()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            var newUserId = Guid.NewGuid();
            var newUserName = "Jane Doe";
            var newBranchId = Guid.NewGuid();
            var newBranchName = "New Branch";
            var newBranchAddress = "456 New St, City";

            // Act
            sale.Update(newUserId, newUserName, newBranchId, newBranchName, newBranchAddress);

            // Assert
            Assert.Equal(newUserId, sale.UserId);
            Assert.Equal(newUserName, sale.UserName);
            Assert.Equal(newBranchId, sale.BranchId);
            Assert.Equal(newBranchName, sale.BranchName);
            Assert.Equal(newBranchAddress, sale.BranchFullAddress);
            Assert.False(sale.Cancelled);
        }

        /// <summary>
        /// Tests that when a sale is cancelled, its Cancelled flag and CancelledAt timestamp are set.
        /// </summary>
        [Fact(DisplayName = "Sale should be marked as cancelled when cancelled")]
        public void Given_ExistingSale_When_Cancelled_Then_ShouldBeMarkedAsCancelled()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            // Act
            sale.CancellSale();

            // Assert
            Assert.True(sale.Cancelled);
            Assert.NotNull(sale.CancelledAt);
        }
    }
}
