using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping.Sales
{
    internal sealed class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(s => s.SaleNumber).IsRequired();
            builder.Property(s => s.TotalItens).IsRequired();
            builder.Property(s => s.TotalSaleAmount).IsRequired();
            builder.Property(s => s.Cancelled).HasDefaultValue(false).IsRequired();
            builder.Property(s => s.CreatedAt).IsRequired();
            builder.Property(s => s.UpdatedAt).IsRequired(false);
            builder.Property(s => s.CancelledAt).IsRequired(false);

            builder.Property(s => s.UserId).IsRequired();
            builder.Property(s => s.UserName).IsRequired().HasMaxLength(50);

            builder.Property(s => s.BranchId).IsRequired();
            builder.Property(s => s.BranchName).IsRequired().HasMaxLength(100);
            builder.Property(s => s.BranchFullAddress).IsRequired().HasMaxLength(150);

            builder.HasMany(p => p.SaleItens)
                .WithOne(p => p.Sale)
                .HasForeignKey(p => p.SaleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
