using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

internal class SaleConfiguraton : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        ConfigureId(builder);
        ConfigureSaleNumber(builder);
        ConfigureDate(builder);
        ConfigureCustomerId(builder);
        ConfigureCustomerName(builder);
        ConfigureBranchId(builder);
        ConfigureBranchName(builder);
        ConfigureIsCancelled(builder);
        ConfigureTotalAmount(builder);
        ConfigureItems(builder);

        builder.HasQueryFilter(s => !s.IsCancelled);
    }

    private static void ConfigureItems(EntityTypeBuilder<Sale> builder)
    {
        builder
            .HasMany(s => s.Items)
            .WithOne()
            .HasForeignKey("SALE_ID")
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void ConfigureTotalAmount(EntityTypeBuilder<Sale> builder)
    {
        builder
            .Property(s => s.TotalAmount)
            .HasColumnName("TOTAL_AMOUNT")
            .HasColumnType("DECIMAL(18, 2)");
    }

    private static void ConfigureIsCancelled(EntityTypeBuilder<Sale> builder)
    {
        builder
            .Property(s => s.IsCancelled)
            .HasColumnName("IS_CANCELLED")
            .HasColumnType("BOOLEAN")
            .IsRequired();
    }

    private static void ConfigureBranchName(EntityTypeBuilder<Sale> builder)
    {
        builder
            .Property(s => s.BranchName)
            .HasColumnName("BRANCH_NAME")
            .HasColumnType("VARCHAR")
            .HasMaxLength(100)
            .IsRequired();
    }

    private static void ConfigureBranchId(EntityTypeBuilder<Sale> builder)
    {
        builder
            .Property(s => s.BranchId)
            .HasColumnName("BRANCH_ID")
            .HasColumnType("VARCHAR")
            .HasMaxLength(50)
            .IsRequired();
    }

    private static void ConfigureCustomerName(EntityTypeBuilder<Sale> builder)
    {
        builder
            .Property(s => s.CustomerName)
            .HasColumnName("CUSTOMER_NAME")
            .HasColumnType("VARCHAR")
            .HasMaxLength(100)
            .IsRequired();
    }

    private static void ConfigureCustomerId(EntityTypeBuilder<Sale> builder)
    {
        builder
            .Property(s => s.CustomerId)
            .HasColumnName("CUSTOMER_ID")
            .HasMaxLength(50)
            .IsRequired();
    }

    private static void ConfigureDate(EntityTypeBuilder<Sale> builder)
    {
        builder
            .Property(s => s.Date)
            .HasColumnName("DATE")
            .IsRequired();
    }

    private static void ConfigureSaleNumber(EntityTypeBuilder<Sale> builder)
    {
        builder
            .Property(s => s.SaleNumber)
            .HasColumnName("SALE_NUMBER")
            .HasColumnType("VARCHAR")
            .HasMaxLength(50)
            .IsRequired();
    }

    private static void ConfigureId(EntityTypeBuilder<Sale> builder)
    {
        builder
            .HasKey(s => s.Id);

        builder
            .Property(s => s.Id)
            .HasColumnName("ID")
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");
    }
}
