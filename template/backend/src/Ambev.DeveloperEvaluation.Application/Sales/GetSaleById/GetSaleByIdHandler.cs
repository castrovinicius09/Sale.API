using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById
{
    /// <summary>
    /// Handler for processing GetSaleById requests
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public sealed class GetSaleByIdHandler(
         ISaleRepository saleRepository,
         IMapper mapper) : IRequestHandler<GetSaleByIdQuery, GetSaleByIdResult>
    {
        private readonly ISaleRepository _saleRepository = saleRepository;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Handles the GetSaleByIdQuery request
        /// </summary>
        /// <param name="request">The GetSaleByIdQuery query</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The detailed sale with items if found</returns>
        public async Task<GetSaleByIdResult> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetSaleByIdValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

            return _mapper.Map<GetSaleByIdResult>(sale);
        }
    }
}
