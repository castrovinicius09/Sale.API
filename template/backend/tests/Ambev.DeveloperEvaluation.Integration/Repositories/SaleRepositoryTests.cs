using Ambev.DeveloperEvaluation.Integration.Common;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.Support.Domain.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Repositories
{
    /// <summary>
    /// Contains integration tests for the sale repository class.
    /// Tests cover repository behaviour with memory database.
    /// </summary>
    public class SaleRepositoryTests
    {
        private readonly DefaultContext _context;
        private readonly SaleRepository _repository;

        public SaleRepositoryTests()
        {
            _context = MockingData.CreateInMemoryContext();
            _repository = new SaleRepository(_context);
        }

        /// <summary>
        /// Tests that paginated retrieval returns the correct number of sales for different page sizes and positions.
        /// </summary>
        [Theory(DisplayName = "Should return correct number of sales for each page")]
        [InlineData(1, 5)]
        [InlineData(2, 10)]
        [InlineData(3, 7)]
        public async Task Given_SalesExist_When_PaginatedRetrievalExecuted_Then_ShouldReturnCorrectPage(int pageNumber, int pageSize)
        {
            // Arrange
            for (int i = 1; i <= 25; i++)
            {
                var sale = SaleTestData.GenerateValidSale();

                await _repository.CreateAsync(sale);
            }

            // Act
            var result = await _repository.GetAllPaginatedAsync(pageNumber, pageSize);

            // Assert
            result.Should().HaveCount(pageSize);
        }

        /// <summary>
        /// Tests that a sale is correctly persisted to the database when created.
        /// </summary>
        [Fact(DisplayName = "Should persist sale when created")]
        public async Task Given_ValidSale_When_Created_Then_ShouldBePersisted()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            // Act
            var result = await _repository.CreateAsync(sale);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();

            var dbSale = await _context.Sales.FindAsync(result.Id);
            dbSale.Should().NotBeNull();
            dbSale.UserName.Should().Be(sale.UserName);
        }

        /// <summary>
        /// Tests that updating a sale modifies its fields and persists the changes.
        /// </summary>
        [Fact(DisplayName = "Should update sale fields when modified")]
        public async Task Given_ExistingSale_When_Updated_Then_FieldsShouldBeModified()
        {
            var sale = SaleTestData.GenerateValidSale();
            await _repository.CreateAsync(sale);

            sale.Update(sale.UserId, "Carlos", sale.BranchId, sale.BranchName, sale.BranchFullAddress);
            var updated = await _repository.UpdateAsync(sale);

            updated.UserName.Should().Be("Carlos");

            var dbSale = await _repository.GetByIdAsync(sale.Id);
            dbSale!.UserName.Should().Be("Carlos");
        }

        /// <summary>
        /// Tests that deleting a sale removes it from the database and returns true.
        /// </summary>
        [Fact(DisplayName = "Should remove sale when deleted")]
        public async Task Given_ExistingSale_When_Deleted_Then_ShouldBeRemoved()
        {
            // Arrange
            var context = MockingData.CreateInMemoryContext();
            var repository = new SaleRepository(context);

            var sale = SaleTestData.GenerateValidSale();

            await repository.CreateAsync(sale);

            // Act
            var deleted = await repository.DeleteAsync(sale.Id);

            // Assert
            deleted.Should().BeTrue();
            var check = await repository.GetByIdAsync(sale.Id);
            check.Should().BeNull();
        }

        /// <summary>
        /// Tests that creating a sale with a cancelled token throws an OperationCanceledException.
        /// </summary>
        [Fact(DisplayName = "Should throw OperationCanceledException when token is cancelled")]
        public async Task Given_CancellationToken_When_CreateAsyncCalled_Then_ShouldThrowException()
        {
            var sale = SaleTestData.GenerateValidSale();
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            Func<Task> act = async () => await _repository.CreateAsync(sale, cts.Token);

            await act.Should().ThrowAsync<OperationCanceledException>();
        }
    }
}
