using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
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
    public class CreateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IBranchService _branchService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly CreateSaleHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleHandlerTests"/> class.
        /// Sets up the test dependencies and creates fake data generators.
        /// </summary>
        public CreateSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _branchService = Substitute.For<IBranchService>();
            _productService = Substitute.For<IProductService>();
            _userService = Substitute.For<IUserService>();

            _handler = new CreateSaleHandler(
                _saleRepository,
                _branchService,
                _productService,
                _userService,
                _mapper);
        }

        /// <summary>
        /// Tests that a valid sale command returns a successful result.
        /// </summary>
        [Fact(DisplayName = "Given valid sale command When handling Then returns success result")]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            // Given
            var command = SaleHandlerTestData.GenerateValidCreateCommand();
            var sale = SaleTestData.GenerateValidSale();
            var result = new CreateSaleResult { Id = sale.Id };

            _mapper.Map<Sale>(command)
                .Returns(sale);

            _saleRepository.CreateAsync(sale, Arg.Any<CancellationToken>())
                .Returns(sale);

            _mapper.Map<CreateSaleResult>(sale)
                .Returns(result);

            // When
            var response = await _handler.Handle(command, CancellationToken.None);

            // Then
            response.Should().NotBeNull();
            response.Id.Should().Be(sale.Id);

            _userService.Received(1).ValidateUser(command.UserId);
            _branchService.Received(1).ValidateBranch(command.BranchId);
            _productService.Received(1).ValidateProduct(Arg.Is<List<Guid>>(ids => ids.SequenceEqual(command.Items.Select(i => i.ProductId))));
        }

        /// <summary>
        /// Tests that an invalid sale command throws a validation exception.
        /// </summary>
        [Fact(DisplayName = "Given invalid sale command When handling Then throws validation exception")]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Given
            var command = SaleHandlerTestData.GenerateInvalidCreateCommand();

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }

        /// <summary>
        /// Tests that a nonexistent user triggers a validation exception.
        /// </summary>
        [Fact(DisplayName = "Given nonexistent user When handling Then throws validation exception")]
        public async Task Handle_NonexistentUser_ThrowsValidationException()
        {
            // Given
            var command = SaleHandlerTestData.GenerateValidCreateCommand();

            _userService.When(s => s.ValidateUser(command.UserId))
                .Do(_ => throw new ValidationException("User not found"));

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>().WithMessage("User not found");
        }

        /// <summary>
        /// Tests that a nonexistent branch triggers a validation exception.
        /// </summary>
        [Fact(DisplayName = "Given nonexistent branch When handling Then throws validation exception")]
        public async Task Handle_NonexistentBranch_ThrowsValidationException()
        {
            // Given
            var command = SaleHandlerTestData.GenerateValidCreateCommand();

            _branchService.When(s => s.ValidateBranch(command.BranchId))
                .Do(_ => throw new ValidationException("Branch not found"));

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>().WithMessage("Branch not found");
        }
    }
}
