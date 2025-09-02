using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Support.Domain.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class GetSaleByIdHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly GetSaleByIdHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSaleByIdHandlerTests"/> class.
        /// Sets up the test dependencies and creates fake data generators.
        /// </summary>
        public GetSaleByIdHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetSaleByIdHandler(_saleRepository, _mapper);
        }

        /// <summary>
        /// Tests that a valid query returns the expected sale result.
        /// </summary>
        [Fact(DisplayName = "Given valid query When handling Then returns sale result")]
        public async Task Handle_ValidQuery_ReturnsSaleResult()
        {
            // Given
            var sale = SaleTestData.GenerateValidSale();
            sale.Id = new Guid("1c0a26c5-506f-497f-b644-854fb7bf4e2d");
            var query = new GetSaleByIdQuery(sale.Id);
            var expectedResult = new GetSaleByIdResult { Id = sale.Id };

            _saleRepository.GetByIdAsync(query.Id, Arg.Any<CancellationToken>())
                .Returns(sale);

            _mapper.Map<GetSaleByIdResult>(sale)
                .Returns(expectedResult);

            // When
            var result = await _handler.Handle(query, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Id.Should().Be(sale.Id);
            await _saleRepository.Received(1).GetByIdAsync(query.Id, Arg.Any<CancellationToken>());
            _mapper.Received(1).Map<GetSaleByIdResult>(Arg.Is<Sale>(s => s.Id == sale.Id));
        }

        /// <summary>
        /// Tests that an invalid query throws a validation exception.
        /// </summary>
        [Fact(DisplayName = "Given invalid query When handling Then throws validation exception")]
        public async Task Handle_InvalidQuery_ThrowsValidationException()
        {
            // Given
            var query = new GetSaleByIdQuery(Guid.Parse("00000000-0000-0000-0000-000000000000"));

            // When
            var act = () => _handler.Handle(query, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        }
    }
}