using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping.Sales
{
    internal sealed class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(si => si.Id);
            builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(si => si.Quantity).IsRequired();
            builder.Property(si => si.UnitPrice).IsRequired().HasPrecision(18, 2);
            builder.Property(si => si.TotalAmount).IsRequired().HasPrecision(18, 2);

            builder.Property(si => si.ProductId).IsRequired();
            builder.Property(si => si.ProductName).IsRequired().HasMaxLength(100);
        }
    }
}
