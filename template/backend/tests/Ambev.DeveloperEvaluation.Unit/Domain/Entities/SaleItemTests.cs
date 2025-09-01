using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Support.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleItemTests
    {
        /// <summary>
        /// Tests that validation passes when a sale item has valid data.
        /// </summary>
        [Fact(DisplayName = "Validation should pass for valid sale item data")]
        public void Given_ValidSaleItemData_When_Validated_Then_ShouldReturnValid()
        {
            // Arrange
            var item = SaleItemTestData.GenerateValidSaleItem();

            // Act
            var result = item.Validate();

            // Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        /// <summary>
        /// Tests that validation fails when a sale item has invalid data.
        /// </summary>
        [Fact(DisplayName = "Validation should fail for invalid sale item data")]
        public void Given_InvalidSaleItemData_When_Validated_Then_ShouldReturnInvalid()
        {
            // Arrange
            var item = SaleItemTestData.GenerateInvalidSaleItem();

            // Act
            var result = item.Validate();

            // Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        /// <summary>
        /// Tests that a sale item is created with correct properties and total amount is calculated.
        /// </summary>
        [Fact(DisplayName = "SaleItem should be created with correct properties")]
        public void Given_ValidData_When_Created_Then_PropertiesShouldBeSet()
        {
            // Arrange
            var validItem = SaleItemTestData.GenerateValidSaleItem();

            // Act
            var item = SaleItem.Create(
                validItem.Quantity,
                validItem.UnitPrice,
                validItem.ProductId,
                validItem.ProductName);

            // Assert
            Assert.Equal(validItem.Quantity, item.Quantity);
            Assert.Equal(validItem.UnitPrice, item.UnitPrice);
            Assert.Equal(validItem.ProductId, item.ProductId);
            Assert.Equal(validItem.ProductName, item.ProductName);
            Assert.True(item.TotalAmount > 0);
        }

        /// <summary>
        /// Tests that when a sale item is updated, its properties and total amount are updated correctly.
        /// </summary>
        [Fact(DisplayName = "SaleItem should update quantity properties when updated")]
        public void Given_ExistingSaleItem_When_Updated_Then_PropertiesShouldChange()
        {
            // Arrange
            var saleItem = SaleItemTestData.GenerateValidSaleItem();

            var newQuantity = 10;
            var newUnitPrice = 25.5m;
            var newProductName = "Updated Product";

            var expectedDiscount = 0.2m;
            var expectedTotal = ((saleItem.Quantity + newQuantity) * newUnitPrice) * (1 - expectedDiscount);

            // Act
            saleItem.Update(newQuantity, newUnitPrice, newProductName);

            // Assert
            Assert.Equal(20, saleItem.Quantity);
            Assert.Equal(newUnitPrice, saleItem.UnitPrice);
            Assert.Equal(newProductName, saleItem.ProductName);
            Assert.Equal(expectedTotal, saleItem.TotalAmount);
        }

        /// <summary>
        /// Tests that attempting to create or update a sale item with quantity above 20 throws an exception.
        /// </summary>
        [Fact(DisplayName = "Quantity above 20 should throw exception")]
        public void Given_QuantityAbove20_When_CreatingOrUpdating_Then_ShouldThrow()
        {
            // Arrange
            var item = SaleItemTestData.GenerateValidSaleItem();
            var invalidQuantity = 21;

            // Act & Assert - Create
            Assert.Throws<InvalidOperationException>(() =>
                SaleItem.Create(invalidQuantity, item.UnitPrice, item.ProductId, item.ProductName));

            // Act & Assert - Update
            Assert.Throws<InvalidOperationException>(() =>
                item.Update(invalidQuantity, item.UnitPrice, item.ProductName));
        }

        /// <summary>
        /// Tests that discount is applied correctly for quantities eligible for 10% discount.
        /// </summary>
        [Fact(DisplayName = "Quantity 4 should apply 10% discount")]
        public void Given_Quantity4_When_CreatedOrUpdated_Then_TotalAmountReflectsDiscount()
        {
            // Arrange
            var quantity = 4;
            var unitPrice = 50m;
            var productId = Guid.NewGuid();
            var productName = "Test Product";

            var expectedDiscount = 0.1m;
            var expectedTotal = (quantity * unitPrice) * (1 - expectedDiscount);

            // Act
            var item = SaleItem.Create(quantity, unitPrice, productId, productName);

            // Assert
            Assert.Equal(expectedTotal, item.TotalAmount);
        }

        /// <summary>
        /// Tests that discount is applied correctly for quantities eligible for 20% discount.
        /// </summary>
        [Fact(DisplayName = "Quantity between 10 and 20 should apply 20% discount")]
        public void Given_QuantityBetween10And20_When_CreatedOrUpdated_Then_TotalAmountReflectsDiscount()
        {
            // Arrange
            var quantity = 15;
            var unitPrice = 30m;
            var productId = Guid.NewGuid();
            var productName = "Test Product";

            var expectedDiscount = 0.2m;
            var expectedTotal = (quantity * unitPrice) * (1 - expectedDiscount);

            // Act
            var item = SaleItem.Create(quantity, unitPrice, productId, productName);

            // Assert
            Assert.Equal(expectedTotal, item.TotalAmount);
        }

        /// <summary>
        /// Tests that no discount is applied for quantities below 4.
        /// </summary>
        [Fact(DisplayName = "Quantity below 4 should have no discount")]
        public void Given_QuantityBelow4_When_CreatedOrUpdated_Then_TotalAmountHasNoDiscount()
        {
            // Arrange
            var quantity = 3;
            var unitPrice = 100m;
            var productId = Guid.NewGuid();
            var productName = "Test Product";

            var expectedTotal = quantity * unitPrice; // no discount

            // Act
            var item = SaleItem.Create(quantity, unitPrice, productId, productName);

            // Assert
            Assert.Equal(expectedTotal, item.TotalAmount);
        }
    }
}
