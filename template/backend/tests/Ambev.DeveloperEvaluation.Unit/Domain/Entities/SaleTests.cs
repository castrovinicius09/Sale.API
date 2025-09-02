using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Support.Domain.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    /// <summary>
    /// Contains unit tests for the Sale entity class.
    /// Tests cover status changes and validation scenarios.
    /// </summary>
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
            var item = validSale.SaleItems.FirstOrDefault();

            // Act
            var sale = Sale.Create(
                validSale.SaleNumber,
                validSale.UserId,
                validSale.UserName,
                validSale.BranchId,
                validSale.BranchName,
                validSale.BranchFullAddress,
                item.Quantity,
                item.UnitPrice,
                item.ProductId,
                item.ProductName);

            // Assert
            Assert.Equal(validSale.SaleNumber, sale.SaleNumber);
            Assert.Equal(validSale.UserId, sale.UserId);
            Assert.Equal(validSale.UserName, sale.UserName);
            Assert.Equal(validSale.BranchId, sale.BranchId);
            Assert.Equal(validSale.BranchName, sale.BranchName);
            Assert.Equal(validSale.BranchFullAddress, sale.BranchFullAddress);
            Assert.False(sale.Cancelled);
            Assert.Equal(validSale.Cancelled, sale.Cancelled);
            Assert.Null(sale.UpdatedAt);
            Assert.Null(sale.CancelledAt);
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
            Assert.NotNull(sale.UpdatedAt);
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

        /// <summary>
        /// Tests that deleting an existing SaleItem removes it from the collection 
        /// and recalculates total items and total sale amount to zero.
        /// </summary>
        [Fact(DisplayName = "DeleteItem should remove existing item and update totals")]
        public void Given_ExistingSaleWithItem_When_DeleteItem_Then_ShouldRemoveItemAndUpdateTotals()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            var item = sale.SaleItems.FirstOrDefault();

            // Act
            sale.DeleteItem(item.ProductId);

            // Assert
            Assert.Empty(sale.SaleItems);
            Assert.Equal(0, sale.TotalItems);
            Assert.Equal(0m, sale.TotalSaleAmount);
        }

        /// <summary>
        /// Tests that calling DeleteItem with a non-existing productId 
        /// does not alter the sale’s item collection or totals.
        /// </summary>
        [Fact(DisplayName = "DeleteItem should not change sale when product does not exist")]
        public void Given_ExistingSale_When_DeleteNonExistingItem_Then_ShouldRemainUnchanged()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            var originalCount = sale.TotalItems;
            var originalTotal = sale.TotalSaleAmount;

            // Act
            sale.DeleteItem(Guid.NewGuid());

            // Assert
            Assert.Equal(originalCount, sale.TotalItems);
            Assert.Equal(originalTotal, sale.TotalSaleAmount);
        }

    }
}
