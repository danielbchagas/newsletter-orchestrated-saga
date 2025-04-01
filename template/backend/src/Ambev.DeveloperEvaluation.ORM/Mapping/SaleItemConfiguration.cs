using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

internal class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        ConfigureId(builder);
        ConfigureProductId(builder);
        ConfigureProductName(builder);
        ConfigureQuantity(builder);
        ConfigureUnitPrice(builder);
        ConfigureDiscount(builder);
        ConfigureTotal(builder);
    }

    private static void ConfigureTotal(EntityTypeBuilder<SaleItem> builder)
    {
        builder
            .Property(s => s.TotalAmount)
            .HasColumnName("TOTAL_AMOUNT")
            .HasColumnType("DECIMAL(18, 2)");
    }

    private static void ConfigureDiscount(EntityTypeBuilder<SaleItem> builder)
    {
        builder
            .Property(si => si.Discount)
            .HasColumnName("DISCOUNT")
            .IsRequired()
            .HasColumnType("DECIMAL(18,2)");
    }

    private static void ConfigureUnitPrice(EntityTypeBuilder<SaleItem> builder)
    {
        builder
            .Property(si => si.UnitPrice)
            .HasColumnName("UNIT_PRICE")
            .HasColumnType("DECIMAL(18,2)")
            .IsRequired();
    }

    private static void ConfigureQuantity(EntityTypeBuilder<SaleItem> builder)
    {
        builder
            .Property(si => si.Quantity)
            .HasColumnName("QUANTITY")
            .HasColumnType("INT")
            .IsRequired();
    }

    private static void ConfigureProductName(EntityTypeBuilder<SaleItem> builder)
    {
        builder
            .Property(si => si.ProductName)
            .HasColumnName("PRODUCT_NAME")
            .HasColumnType("VARCHAR")
            .HasMaxLength(100)
            .IsRequired();
    }

    private static void ConfigureProductId(EntityTypeBuilder<SaleItem> builder)
    {
        builder
            .Property(si => si.ProductId)
            .HasColumnName("PRODUCT_ID")
            .HasColumnType("VARCHAR")
            .IsRequired()
            .HasMaxLength(50);
    }

    private static void ConfigureId(EntityTypeBuilder<SaleItem> builder)
    {
        builder.HasKey(si => si.Id);

        builder
            .Property(s => s.Id)
            .HasColumnName("ID")
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");
    }
}
