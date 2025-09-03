using Ambev.DeveloperEvaluation.Application.Services.Abstractions;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Handler for processing CreateSaleCommand requests
    /// <param name="saleRepository">The user repository</param>
    /// <param name="branchService">The external branch</param>
    /// <param name="productService">The external product</param>
    /// <param name="userService">The user service</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for CreateUserCommand</param>
    /// </summary>
    public sealed class CreateSaleHandler(
        ISaleRepository saleRepository,
        IBranchService branchService,
        IProductService productService,
        IUserService userService,
        IMapper mapper) : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository = saleRepository;

        private readonly IBranchService _branchService = branchService;
        private readonly IProductService _productService = productService;
        private readonly IUserService _userService = userService;

        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Handles the CreateSaleCommand request
        /// </summary>
        /// <param name="command">The CreateSale command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created sale details</returns>
        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            _userService.ValidateUser(command.UserId);
            _branchService.ValidateBranch(command.BranchId);
            _productService.ValidateProduct(command.Items.Select(s => s.ProductId).ToList());

            var sale = _mapper.Map<Sale>(command);
            var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
            var result = _mapper.Map<CreateSaleResult>(createdSale);

            return result;
        }
    }
}
