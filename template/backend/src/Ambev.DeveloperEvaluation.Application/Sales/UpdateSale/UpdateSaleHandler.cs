using Ambev.DeveloperEvaluation.Application.Services.Abstractions;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Handler for processing UpdateSaleCommand requests
    /// <param name="saleRepository">The user repository</param>
    /// <param name="branchService">The external branch</param>
    /// <param name="productService">The external product</param>
    /// <param name="userService">The user service</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for UpdateSaleCommand</param>
    /// </summary>
    public sealed class UpdateSaleHandler(
        ISaleRepository saleRepository,
        IBranchService branchService,
        IProductService productService,
        IUserService userService,
        IMapper mapper) : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository = saleRepository;

        private readonly IBranchService _branchService = branchService;
        private readonly IProductService _productService = productService;
        private readonly IUserService _userService = userService;

        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Handles the UpdateSaleCommand request
        /// </summary>
        /// <param name="command">The UpdateSale command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated sale</returns>
        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with ID {command.Id} not found.");

            _userService.ValidateUser(command.UserId);
            _branchService.ValidateBranch(command.BranchId);
            _productService.ValidateProduct(command.Items.Select(s => s.ProductId).ToList());

            _mapper.Map<UpdateSaleCommand, Sale>(command, sale);

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            var result = _mapper.Map<UpdateSaleResult>(sale);

            return result;
        }
    }
}
