using Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSales;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Support.Domain.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class GetPaginatedSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly GetPaginatedSaleHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaginatedSaleHandlerTests"/> class.
        /// Sets up the test dependencies and creates fake data generators.
        /// </summary>
        public GetPaginatedSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetPaginatedSaleHandler(_saleRepository, _mapper);
        }

        /// <summary>
        /// Tests that a valid paginated sale query returns the expected result.
        /// </summary>
        [Fact(DisplayName = "Given valid query When handling Then returns sales result")]
        public async Task Handle_ValidQuery_ReturnsSalesResultList()
        {
            // Given
            var query = new GetPaginatedSaleQuery(1, 5);
            var sales = Enumerable.Range(1, 5).Select(_ => SaleTestData.GenerateValidSale()).ToList();
            var expectedResult = sales.Select(s => new GetSaleByIdResult { Id = s.Id }).ToList();

            _saleRepository.GetAllPaginatedAsync(query.PageNumber, query.PageSize, Arg.Any<CancellationToken>())
                .Returns(sales);

            _mapper.Map<IEnumerable<GetSaleByIdResult>>(sales)
                .Returns(expectedResult);

            // When
            var result = await _handler.Handle(query, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Should().HaveCount(5);
            result.Select(r => r.Id).Should().BeEquivalentTo(expectedResult.Select(r => r.Id));
            await _saleRepository.Received(1).GetAllPaginatedAsync(query.PageNumber, query.PageSize, Arg.Any<CancellationToken>());
            _mapper.Received(1).Map<IEnumerable<GetSaleByIdResult>>(Arg.Is<IEnumerable<Sale>>(list => list.SequenceEqual(sales)));
        }

        /// <summary>
        /// Tests that an invalid paginated sale query throws a validation exception.
        /// </summary>
        [Theory(DisplayName = "Given invalid query When handling Then throws validation exception")]
        [InlineData(-1, -10)]
        [InlineData(0, default)]
        public async Task Handle_InvalidQuery_ThrowsValidationException(int pageNumber, int pageSize)
        {
            // Given
            var query = new GetPaginatedSaleQuery(1, default); // Missing page number and size

            // When
            var act = () => _handler.Handle(query, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        }
    }
}