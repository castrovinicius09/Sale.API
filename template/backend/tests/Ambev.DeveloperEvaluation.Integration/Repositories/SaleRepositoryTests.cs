using Ambev.DeveloperEvaluation.Integration.Common;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.Support.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Repositories
{
    public class SaleRepositoryTests
    {
        private readonly DefaultContext _context;
        private readonly SaleRepository _repository;

        public SaleRepositoryTests()
        {
            _context = MockingData.CreateInMemoryContext();
            _repository = new SaleRepository(_context);
        }

        //[Fact]
        //public async Task GetByAllAsync_ShouldReturnPaginatedSales()
        //{
        //    var context = CreateInMemoryContext();
        //    var repository = new SaleRepository(context);

        //    for (int i = 0; i < 25; i++)
        //    {
        //        var sale = SaleBuilder.Default().WithSaleNumber(1000 + i).Build();
        //        await repository.CreateAsync(sale);
        //    }

        //    var pageSize = 10;
        //    var pageNumber = 2;

        //    var paginatedSales = await context.Sales
        //        .OrderBy(s => s.SaleNumber)
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToListAsync();

        //    paginatedSales.Should().HaveCount(10);
        //    paginatedSales.First().SaleNumber.Should().Be(1010);
        //}

        [Fact]
        public async Task CreateAsync_ShouldPersistSale()
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

        [Fact]
        public async Task UpdateAsync_ShouldModifySaleFields()
        {
            var sale = SaleTestData.GenerateValidSale();
            await _repository.CreateAsync(sale);

            sale.Update(sale.UserId, "Carlos", sale.BranchId, sale.BranchName, sale.BranchFullAddress);
            var updated = await _repository.UpdateAsync(sale);

            updated.UserName.Should().Be("Carlos");

            var dbSale = await _repository.GetByIdAsync(sale.Id);
            dbSale!.UserName.Should().Be("Carlos");
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveSale()
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

        [Fact]
        public async Task CreateAsync_ShouldThrowIfCancelled()
        {
            var sale = SaleTestData.GenerateValidSale();
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            Func<Task> act = async () => await _repository.CreateAsync(sale, cts.Token);

            await act.Should().ThrowAsync<OperationCanceledException>();
        }
    }
}
