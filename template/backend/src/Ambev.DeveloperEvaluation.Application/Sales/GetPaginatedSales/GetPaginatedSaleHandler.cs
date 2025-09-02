using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSales
{
    /// <summary>
    /// Handler for processing GetPaginatedSaleQuery requests
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public sealed class GetPaginatedSaleHandler(
         ISaleRepository saleRepository,
         IMapper mapper) : IRequestHandler<GetPaginatedSaleQuery, IEnumerable<GetSaleByIdResult>>
    {
        private readonly ISaleRepository _saleRepository = saleRepository;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Handles the GetPaginatedSaleQuery request
        /// </summary>
        /// <param name="request">The GetUser command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The user details if found</returns>
        public async Task<IEnumerable<GetSaleByIdResult>> Handle(GetPaginatedSaleQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetPaginatedSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sales = await _saleRepository.GetAllPaginatedAsync(request.PageNumber, request.PageSize, cancellationToken);

            return _mapper.Map<IEnumerable<GetSaleByIdResult>>(sales);
        }
    }
}
