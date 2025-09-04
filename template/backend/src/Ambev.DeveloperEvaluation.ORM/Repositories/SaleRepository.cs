using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Implementation of ISaleRepository using Entity Framework Core
    /// </summary>
    public sealed class SaleRepository(DefaultContext context) : ISaleRepository
    {
        private readonly DefaultContext _context = context;

        /// <summary>
        /// Retrieves a sale by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the sale</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale if found, null otherwise</returns>
        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sales.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        /// <summary>
        /// Retrieves all sales paginated
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Sale list if exist, null otherwise</returns>
        public async Task<IEnumerable<Sale>> GetAllPaginatedAsync(
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Creates a new Sale in the repository
        /// </summary>
        /// <param name="sale">The sale to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created sale</returns>
        public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _context.Sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        /// <summary>
        /// Update an existing Sale in the repository
        /// </summary>
        /// <param name="sale">The sale to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated sale</returns>
        public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        /// <summary>
        /// Cancell a existing sale
        /// </summary>
        /// <param name="id">The unique identifier of the sale to cancell</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if the sale was cancelled, false if not found</returns>
        public async Task<bool> CancellAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sale = await GetByIdAsync(id, cancellationToken);
            if (sale == null)
                return false;

            sale.CancellSale();

            _context.Update(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        /// <summary>
        /// Check if a sale with the given sale number exists
        /// </summary>
        /// <param name="saleNumber">The sale number to check</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Return a sale object if exist</returns>
        public async Task<Sale?> GetBySaleNumber(long saleNumber, CancellationToken cancellationToken = default)
        {
            return await _context.Sales.FirstOrDefaultAsync(o => o.SaleNumber == saleNumber, cancellationToken);
        }
    }
}
