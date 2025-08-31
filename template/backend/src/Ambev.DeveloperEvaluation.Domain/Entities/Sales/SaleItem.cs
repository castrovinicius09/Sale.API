using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation.Sales;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Sales
{
    public class SaleItem : BaseEntity
    {
        private decimal _totalAmount;

        private SaleItem(
            int quantity,
            decimal unitPrice,
            Guid productId,
            string productName)
        {
            Quantity = quantity;
            UnitPrice = unitPrice;
            ProductId = productId;
            ProductName = productName;

            CalculateTotalAmount();
        }

        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalAmount => _totalAmount;

        //public bool Cancelled { get; private set; }
        //public DateTime? CancelledAt { get; private set; }

        public Guid ProductId { get; private set; } //external ID
        public string ProductName { get; private set; } //denormalization property

        public ValidationResultDetail Validate()
        {
            var validator = new SaleItemValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }

        public static SaleItem Create(
            int quantity,
            decimal unitPrice,
            Guid productId,
            string productName)
        {
            return new SaleItem(
                quantity,
                unitPrice,
                productId,
                productName);
        }

        public void Update(
            int quantity,
            decimal unitPrice,
            string productName)
        {
            Quantity += quantity;
            UnitPrice = unitPrice;
            ProductName = productName;

            CalculateTotalAmount();
        }

        private void CalculateTotalAmount()
        {
            var discount = CalculateDiscount();
            var totalAmount = (UnitPrice * Quantity);
            _totalAmount = totalAmount - (totalAmount * discount);
        }

        private decimal CalculateDiscount()
        {
            return Quantity switch
            {
                >= 4 and <= 9 => 0.1m, //10% discount
                >= 10 and <= 20 => 0.2m, //20% discount
                > 20 => throw new InvalidOperationException("Cannot sell above 20 identical items."),
                _ => 0
            };
        }
    }
}
