using Ambev.DeveloperEvaluation.Domain.Validation.Sales;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation.Sales
{
    public class SaleValidatorTest
    {
        /// <summary>
        /// Contains unit tests for the SaleValidator class.
        /// Tests cover validation of all sale properties including SaleNumber,
        /// CreateAt, UserId, UserName, BranchId, BranchName, and BranchFullAddress.
        /// </summary>
        public class SaleValidatorTests
        {
            private readonly SaleValidator _validator;

            public SaleValidatorTests()
            {
                _validator = new SaleValidator();
            }

            [Fact(DisplayName = "Valid sale should pass all validation rules")]
            public void Given_ValidSale_When_Validated_Then_ShouldNotHaveErrors()
            {
                // Arrange
                var sale = SaleTestData.GenerateValidSale();

                // Act
                var result = _validator.TestValidate(sale);

                // Assert
                result.ShouldNotHaveAnyValidationErrors();
            }

            [Fact(DisplayName = "SaleNumber less than or equal to zero should fail validation")]
            public void Given_InvalidSaleNumber_When_Validated_Then_ShouldHaveError()
            {
                // Arrange
                var sale = SaleTestData.GenerateInvalidSale();

                // Act
                var result = _validator.TestValidate(sale);

                // Assert
                result.ShouldHaveValidationErrorFor(x => x.SaleNumber);
            }

            [Fact(DisplayName = "Empty UserId should fail validation")]
            public void Given_EmptyUserId_When_Validated_Then_ShouldHaveError()
            {
                // Arrange
                var sale = SaleTestData.GenerateValidSale();
                sale.Update(Guid.Empty, sale.UserName, sale.BranchId, sale.BranchName, sale.BranchFullAddress);

                // Act
                var result = _validator.TestValidate(sale);

                // Assert
                result.ShouldHaveValidationErrorFor(x => x.UserId);
            }

            [Fact(DisplayName = "Null or empty UserName should fail validation")]
            public void Given_InvalidUserName_When_Validated_Then_ShouldHaveError()
            {
                // Arrange
                var sale = SaleTestData.GenerateValidSale();
                sale.Update(sale.UserId, "", sale.BranchId, sale.BranchName, sale.BranchFullAddress);

                // Act
                var result = _validator.TestValidate(sale);

                // Assert
                result.ShouldHaveValidationErrorFor(x => x.UserName);
            }

            [Fact(DisplayName = "UserName longer than 50 characters should fail validation")]
            public void Given_UserNameTooLong_When_Validated_Then_ShouldHaveError()
            {
                // Arrange
                var sale = SaleTestData.GenerateValidSale();
                var longName = UserTestData.GenerateLongUsername();
                sale.Update(sale.UserId, longName, sale.BranchId, sale.BranchName, sale.BranchFullAddress);

                // Act
                var result = _validator.TestValidate(sale);

                // Assert
                result.ShouldHaveValidationErrorFor(x => x.UserName);
            }

            [Fact(DisplayName = "Empty BranchId should fail validation")]
            public void Given_EmptyBranchId_When_Validated_Then_ShouldHaveError()
            {
                // Arrange
                var sale = SaleTestData.GenerateValidSale();
                sale.Update(sale.UserId, sale.UserName, Guid.Empty, sale.BranchName, sale.BranchFullAddress);

                // Act
                var result = _validator.TestValidate(sale);

                // Assert
                result.ShouldHaveValidationErrorFor(x => x.BranchId);
            }

            [Fact(DisplayName = "Empty BranchName should fail validation")]
            public void Given_EmptyBranchName_When_Validated_Then_ShouldHaveError()
            {
                // Arrange
                var sale = SaleTestData.GenerateValidSale();
                sale.Update(sale.UserId, sale.UserName, sale.BranchId, "", sale.BranchFullAddress);

                // Act
                var result = _validator.TestValidate(sale);

                // Assert
                result.ShouldHaveValidationErrorFor(x => x.BranchName);
            }

            [Fact(DisplayName = "Too long BranchName should fail validation")]
            public void Given_LongBranchName_When_Validated_Then_ShouldHaveError()
            {
                // Arrange
                var sale = SaleTestData.GenerateValidSale();
                var longBranchName = SaleTestData.GenerateLongBranchName();
                sale.Update(sale.UserId, sale.UserName, sale.BranchId, longBranchName, sale.BranchFullAddress);

                // Act
                var result = _validator.TestValidate(sale);

                // Assert
                result.ShouldHaveValidationErrorFor(x => x.BranchName);
            }

            [Fact(DisplayName = "Empty BranchFullAddress should fail validation")]
            public void Given_EmptyBranchAddress_When_Validated_Then_ShouldHaveError()
            {
                // Arrange
                var sale = SaleTestData.GenerateValidSale();
                sale.Update(sale.UserId, sale.UserName, sale.BranchId, sale.BranchName, "");

                // Act
                var result = _validator.TestValidate(sale);

                // Assert
                result.ShouldHaveValidationErrorFor(x => x.BranchFullAddress);
            }

            [Fact(DisplayName = "Too long BranchFullAddress should fail validation")]
            public void Given_LongBranchAddress_When_Validated_Then_ShouldHaveError()
            {
                // Arrange
                var sale = SaleTestData.GenerateValidSale();
                var longBranchAddress = SaleTestData.GenerateLongBranchAddress();
                sale.Update(sale.UserId, sale.UserName, sale.BranchId, sale.BranchName, longBranchAddress);

                // Act
                var result = _validator.TestValidate(sale);

                // Assert
                result.ShouldHaveValidationErrorFor(x => x.BranchFullAddress);
            }
        }
    }
}
