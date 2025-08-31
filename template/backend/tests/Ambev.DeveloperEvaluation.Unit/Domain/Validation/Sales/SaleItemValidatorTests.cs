using Ambev.DeveloperEvaluation.Domain.Validation.Sales;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation.Sales
{
    public class SaleItemValidatorTests
    {
        private readonly SaleItemValidator _validator;

        public SaleItemValidatorTests()
        {
            _validator = new SaleItemValidator();
        }

        /// <summary>
        /// Tests that a valid sale item passes all validation rules.
        /// </summary>
        [Fact(DisplayName = "Valid sale item should pass all validation rules")]
        public void Given_ValidSaleItem_When_Validated_Then_ShouldNotHaveErrors()
        {
            // Arrange
            var saleItem = SaleItemTestData.GenerateValidSaleItem();

            // Act
            var result = _validator.TestValidate(saleItem);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests that a sale item with quantity less than or equal to zero fails validation.
        /// </summary>
        [Fact(DisplayName = "Quantity less than or equal to zero should fail validation")]
        public void Given_InvalidQuantity_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var saleItem = SaleItemTestData.GenerateInvalidSaleItem();

            // Act
            var result = _validator.TestValidate(saleItem);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Quantity);
        }

        /// <summary>
        /// Tests that a sale item with unit price less than or equal to zero fails validation.
        /// </summary>
        [Fact(DisplayName = "UnitPrice less than or equal to zero should fail validation")]
        public void Given_InvalidUnitPrice_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var saleItem = SaleItemTestData.GenerateInvalidSaleItem();

            // Act
            var result = _validator.TestValidate(saleItem);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UnitPrice);
        }

        /// <summary>
        /// Tests that a sale item with total amount less than or equal to zero fails validation.
        /// </summary>
        [Fact(DisplayName = "TotalAmount less than or equal to zero should fail validation")]
        public void Given_InvalidTotalAmount_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var saleItem = SaleItemTestData.GenerateInvalidSaleItem();

            // Act
            var result = _validator.TestValidate(saleItem);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.TotalAmount);
        }

        /// <summary>
        /// Tests that a sale item with empty ProductId fails validation.
        /// </summary>
        [Fact(DisplayName = "Empty ProductId should fail validation")]
        public void Given_EmptyProductId_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var saleItem = SaleItemTestData.GenerateInvalidSaleItem();

            // Act
            var result = _validator.TestValidate(saleItem);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ProductId);
        }

        /// <summary>
        /// Tests that a sale item with empty ProductName fails validation.
        /// </summary>
        [Fact(DisplayName = "Empty ProductName should fail validation")]
        public void Given_EmptyProductName_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var saleItem = SaleItemTestData.GenerateInvalidSaleItem();

            // Act
            var result = _validator.TestValidate(saleItem);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ProductName);
        }

        /// <summary>
        /// Tests that a sale item with ProductName longer than 100 characters fails validation.
        /// </summary>
        [Fact(DisplayName = "Too long ProductName should fail validation")]
        public void Given_TooLongProductName_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var saleItem = SaleItemTestData.GenerateInvalidSaleItem();

            // Act
            var result = _validator.TestValidate(saleItem);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ProductName);
        }
    }
}
