using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Sales
{
    public class Sale : BaseEntity
    {
        public Sale(
            int saleNumber,
            Guid userId,
            string userName,
            Guid branchId,
            string branchName,
            string branchAddress)
        {
            SaleNumber = saleNumber;
            UserId = userId;
            UserName = userName;
            BranchId = branchId;
            BranchName = branchName;
            BranchAddress = branchAddress;
            Cancelled = false;
            CreateAt = DateTime.UtcNow;
        }

        public int SaleNumber { get; private set; }
        public bool Cancelled { get; private set; }
        public DateTime CreateAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? CancelledAt { get; private set; }
        //public int TotalItems { get; set; }

        //public decimal TotalSaleAMount { get; private set; }

        public Guid UserId { get; private set; }
        public string UserName { get; private set; }

        public Guid BranchId { get; private set; }
        public string BranchName { get; private set; }
        public string BranchAddress { get; private set; }

        //public IReadOnlyList<SaleItem> SaleItens { get; set; }

        public static Sale Create(
            int saleNumber,
            Guid userId,
            string userName,
            Guid branchId,
            string branchName,
            string branchAddress)
        {
            //TODO: Create SaleCreatedEvent
            return new Sale(
                saleNumber,
                userId,
                userName,
                branchId,
                branchName,
                branchAddress);
        }

        public void Update(
            int saleNumber,
            Guid userId,
            string userName,
            Guid branchId,
            string branchName,
            string branchAddress)
        {
            SaleNumber = saleNumber;
            UserId = userId;
            UserName = userName;
            BranchId = branchId;
            BranchName = branchName;
            BranchAddress = branchAddress;
            Cancelled = false;
            CreateAt = DateTime.UtcNow;
            //TODO: Create SaleUpdatedEvent
        }

        public void CancellSale()
        {
            Cancelled = true;
            CancelledAt = DateTime.UtcNow;
            //TODO: Create SaleCancelledEvent
        }
    }
}
