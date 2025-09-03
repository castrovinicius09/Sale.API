namespace Ambev.DeveloperEvaluation.Application.Dtos.Sales
{
    /// <summary>
    /// Data transfer object representing an individual item in a sale.
    /// </summary>
    /// <remarks>
    /// This DTO is used within the <see cref="CreateSaleCommand"/> to capture the details 
    /// of each product being sold, including quantity, unit price, product identifier, and name.
    ///
    /// It is typically validated by the <see cref="CreateSaleCommandValidator"/> to ensure 
    /// that all fields are correctly populated and meet the business rules for item inclusion.
    /// </remarks>
    public record SaleItemDto
    {
        /// <summary>
        /// Gets the quantity of the product being sold.
        /// </summary>
        public int Quantity { get; }

        /// <summary>
        /// Gets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; }

        /// <summary>
        /// Gets the unique identifier of the product.
        /// </summary>
        public Guid ProductId { get; }

        /// <summary>
        /// Gets the name of the product.
        /// </summary>
        public string ProductName { get; } = string.Empty;
    }
}
