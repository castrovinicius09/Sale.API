using Ambev.DeveloperEvaluation.Domain.Entities.Sales;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Repository interface for Sale entity operations
    /// </summary>
    public interface ISaleRepository
    {
        /// <summary>
        /// Retrieves a sale by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the sale</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale if found, null otherwise</returns>
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all sales paginated
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Sale list if exist, null otherwise</returns>
        Task<IEnumerable<Sale>> GetAllPaginatedAsync(
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new Sale in the repository
        /// </summary>
        /// <param name="sale">The sale to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created sale</returns>
        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update an existing Sale in the repository
        /// </summary>
        /// <param name="sale">The sale to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated sale</returns>
        Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);

        /// <summary>
        /// Cancell a existing sale
        /// </summary>
        /// <param name="id">The unique identifier of the sale to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if the sale was cancelled, false if not found</returns>
        Task<bool> CancellAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Check if a sale with the given sale number exists
        /// </summary>
        /// <param name="saleNumber">The sale number to check</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Return a sale object if exist</returns>
        Task<Sale?> GetBySaleNumber(long saleNumber, CancellationToken cancellationToken = default);
    }
}
