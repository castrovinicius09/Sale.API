using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancellSale
{
    public sealed class CancellSaleHandler(
        ISaleRepository saleRepository,
        IMapper mapper) : IRequestHandler<CancellSaleCommand, bool>
    {
        private readonly ISaleRepository _saleRepository = saleRepository;

        private readonly IMapper _mapper = mapper;

        public async Task<bool> Handle(CancellSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CancellSaleValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var cancelled = await _saleRepository.CancellAsync(command.Id, cancellationToken);
            if (!cancelled)
                throw new KeyNotFoundException($"Sale with ID {command.Id} not found.");

            return cancelled;
        }
    }
}
