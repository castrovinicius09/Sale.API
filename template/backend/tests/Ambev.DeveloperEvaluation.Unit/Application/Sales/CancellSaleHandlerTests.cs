using Ambev.DeveloperEvaluation.Application.Sales.CancellSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    /// <summary>
    /// Contains unit tests for the <see cref="CancellSaleHandler"/> class.
    /// </summary>
    public class CancellSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly CancellSaleHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="CancellSaleHandlerTests"/> class.
        /// Sets up the test dependencies and creates fake data generators.
        /// </summary>
        public CancellSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new CancellSaleHandler(_saleRepository, _mapper);
        }

        /// <summary>
        /// Tests that a valid cancellation command returns true when sale is successfully deleted.
        /// </summary>
        [Fact(DisplayName = "Given valid cancellation command When handling Then returns true")]
        public async Task Handle_ValidCommand_ReturnsTrue()
        {
            // Given
            var command = SaleHandlerTestData.GenerateValidCancellCommand();

            _saleRepository.CancellAsync(command.Id, Arg.Any<CancellationToken>())
                .Returns(true);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().BeTrue();
            await _saleRepository.Received(1).CancellAsync(command.Id, Arg.Any<CancellationToken>());
        }

        /// <summary>
        /// Tests that an invalid cancellation command throws a validation exception.
        /// </summary>
        [Fact(DisplayName = "Given invalid cancellation command When handling Then throws validation exception")]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Given
            var command = new CancellSaleCommand(); // Missing ID

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }

        /// <summary>
        /// Tests that when sale is not found, a key not found exception is thrown.
        /// </summary>
        [Fact(DisplayName = "Given nonexistent sale When handling Then throws key not found exception")]
        public async Task Handle_SaleNotFound_ThrowsKeyNotFoundException()
        {
            // Given
            var command = SaleHandlerTestData.GenerateValidCancellCommand();

            _saleRepository.CancellAsync(command.Id, Arg.Any<CancellationToken>())
                .Returns(false);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Sale with ID {command.Id} not found.");
        }
    }
}
