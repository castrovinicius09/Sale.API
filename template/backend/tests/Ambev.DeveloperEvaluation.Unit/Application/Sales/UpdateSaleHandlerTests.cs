using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Services.Abstractions;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Support.Domain.TestData;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class UpdateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IBranchService _branchService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly UpdateSaleHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSaleHandlerTests"/> class.
        /// Sets up the test dependencies and creates fake data generators.
        /// </summary>
        public UpdateSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _branchService = Substitute.For<IBranchService>();
            _productService = Substitute.For<IProductService>();
            _userService = Substitute.For<IUserService>();
            _mapper = Substitute.For<IMapper>();
            _handler = new UpdateSaleHandler(_saleRepository, _branchService, _productService, _userService, _mapper);
        }

        /// <summary>
        /// Tests that a valid update command returns the updated sale result.
        /// </summary>
        [Fact(DisplayName = "Given valid update command When handling Then returns updated sale result")]
        public async Task Handle_ValidCommand_ReturnsUpdatedSaleResult()
        {
            // Given
            var command = SaleHandlerTestData.GenerateValidUpdateCommand();

            var existingSale = SaleTestData.GenerateValidSale();
            existingSale.Id = command.Id;

            var expectedResult = new UpdateSaleResult { Id = command.Id };

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                .Returns(existingSale);

            _saleRepository.UpdateAsync(existingSale, Arg.Any<CancellationToken>())
                .Returns(existingSale);

            _mapper.Map<UpdateSaleCommand, Sale>(command, existingSale);

            _mapper.Map<UpdateSaleResult>(existingSale)
                .Returns(expectedResult);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Id.Should().Be(command.Id);

            _userService.Received(1).ValidateUser(command.UserId);
            _branchService.Received(1).ValidateBranch(command.BranchId);
            _productService.Received(1).ValidateProduct(Arg.Is<List<Guid>>(ids => ids.SequenceEqual(command.Items.Select(i => i.ProductId))));

            _mapper.Received(2).Map<UpdateSaleCommand, Sale>(command, existingSale);
            _mapper.Received(1).Map<UpdateSaleResult>(existingSale);
        }

        /// <summary>
        /// Tests that an invalid update command throws a validation exception.
        /// </summary>
        [Fact(DisplayName = "Given invalid update command When handling Then throws validation exception")]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Given
            var command = new UpdateSaleCommand();

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }

        /// <summary>
        /// Tests that a nonexistent sale throws a key not found exception.
        /// </summary>
        [Fact(DisplayName = "Given nonexistent sale When handling Then throws key not found exception")]
        public async Task Handle_SaleNotFound_ThrowsKeyNotFoundException()
        {
            // Given
            var command = SaleHandlerTestData.GenerateValidUpdateCommand();

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                .Returns((Sale)null);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Sale with ID {command.Id} not found.");
        }
    }
}
