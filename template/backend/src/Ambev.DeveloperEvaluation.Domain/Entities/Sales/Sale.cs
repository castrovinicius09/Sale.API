using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation.Sales;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Sales
{
    public class Sale : BaseEntity
    {
        private int _totalItems;
        private decimal _totalSaleAmount;
        private List<SaleItem> _saleItems = new();

        private Sale() { }

        private Sale(
            long saleNumber,
            Guid userId,
            string userName,
            Guid branchId,
            string branchName,
            string branchAddress)
        //int quantity,
        //decimal unitPrice,
        //Guid productId,
        //string productName)
        {
            SaleNumber = saleNumber;
            UserId = userId;
            UserName = userName;
            BranchId = branchId;
            BranchName = branchName;
            BranchFullAddress = branchAddress;
            Cancelled = false;
            CreatedAt = DateTime.UtcNow;

            //AddItem(quantity, unitPrice, productId, productName);
        }

        public long SaleNumber { get; private set; } //cannot be updated
        public bool Cancelled { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? CancelledAt { get; private set; }

        public Guid UserId { get; private set; } //external ID
        public string UserName { get; private set; } //denormalization property

        public Guid BranchId { get; private set; } //external ID
        public string BranchName { get; private set; } //denormalization property
        public string BranchFullAddress { get; private set; } //denormalization property

        public int TotalItems => _totalItems;
        public decimal TotalSaleAmount => _totalSaleAmount;
        public IReadOnlyList<SaleItem> SaleItems => _saleItems;

        public ValidationResultDetail Validate()
        {
            var validator = new SaleValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }

        public static Sale Create(
            long saleNumber,
            Guid userId,
            string userName,
            Guid branchId,
            string branchName,
            string branchAddress)
        /*saleitem properties*/
        //int quantity,
        //decimal unitPrice,
        //Guid productId,
        //string productName)
        {
            //TODO: Create SaleCreatedEvent
            return new Sale(
                saleNumber,
                userId,
                userName,
                branchId,
                branchName,
                branchAddress);
            //quantity,
            //unitPrice,
            //productId,
            //productName);
        }

        public void Update(
            Guid userId,
            string userName,
            Guid branchId,
            string branchName,
            string branchAddress)
        {
            UserId = userId;
            UserName = userName;
            BranchId = branchId;
            BranchName = branchName;
            BranchFullAddress = branchAddress;
            Cancelled = false;
            UpdatedAt = DateTime.UtcNow;
            //TODO: Create SaleUpdatedEvent
        }

        public void CancellSale()
        {
            Cancelled = true;
            CancelledAt = DateTime.UtcNow;
            //TODO: Create SaleCancelledEvent
        }

        public void DeleteItem(Guid productId)
        {
            var item = _saleItems.FirstOrDefault(i => i.ProductId == productId);
            if (item is null) return;

            _saleItems.Remove(item);
            _totalItems = _saleItems.Count;
            _totalSaleAmount = _saleItems.Sum(i => i.TotalAmount);

            //fire event SaleItemDeletedEvent
        }

        public void AddItem(
            int quantity,
            decimal unitPrice,
            Guid productId,
            string productName)
        {
            var item = _saleItems.FirstOrDefault(i => i.ProductId == productId);
            if (item is not null)
            {
                item.Update(quantity, unitPrice, productName);
            }
            else
            {
                item = SaleItem.Create(quantity, unitPrice, productId, productName);
            }

            _saleItems.Add(item);
            _totalItems = _saleItems.Sum(s => s.Quantity);
            _totalSaleAmount = _saleItems.Sum(i => i.TotalAmount);
        }
    }
}
