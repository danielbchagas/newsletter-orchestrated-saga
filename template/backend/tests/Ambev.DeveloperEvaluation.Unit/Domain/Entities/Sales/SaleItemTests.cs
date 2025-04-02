using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoFixture;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.Sales;

public class SaleItemTests
{
    [Fact]
    public void ApplyDiscount_AppliesDiscount_WhenQuantityIsFourOrMore()
    {
        // Arrange
        var saleItem = new Fixture()
            .Build<SaleItem>()
            .With(si => si.Quantity, 4)
            .With(si => si.UnitPrice, 100m)
            .Create();
        
        // Act
        var result = saleItem.ApplyDiscount();
        
        // Assert
        Assert.Equal(360m, result);
    }

    [Fact]
    public void ApplyDiscount_DoesNotApplyDiscount_WhenQuantityIsLessThanFour()
    {
        // Arrange
        var saleItem = new Fixture()
            .Build<SaleItem>()
            .With(si => si.Quantity, 3)
            .With(si => si.UnitPrice, 100m)
            .Create();
        
        // Act
        var result = saleItem.ApplyDiscount();
        
        // Assert
        Assert.Equal(300m, result);
    }

    [Fact]
    public void ApplyDiscount_CalculatesTotalAmountCorrectly_WithNoDiscount()
    {
        // Arrange
        var saleItem = new Fixture()
            .Build<SaleItem>()
            .With(si => si.Quantity, 2)
            .With(si => si.UnitPrice, 50m)
            .Create();
        
        // Act
        var result = saleItem.ApplyDiscount();
        
        // Assert
        Assert.Equal(100m, result);
    }

    [Fact]
    public void ApplyDiscount_CalculatesTotalAmountCorrectly_WithDiscount()
    {
        // Arrange
        var saleItem = new Fixture()
            .Build<SaleItem>()
            .With(si => si.Quantity, 5)
            .With(si => si.UnitPrice, 20m)
            .Create();
        
        // Act
        var result = saleItem.ApplyDiscount();
        
        // Assert
        Assert.Equal(90m, result);
    }

    [Fact]
    public void ApplyDiscount_ReturnsZero_WhenQuantityIsZero()
    {
        // Arrange
        var saleItem = new Fixture()
            .Build<SaleItem>()
            .With(si => si.Quantity, 0)
            .With(si => si.UnitPrice, 100m)
            .Create();
        
        // Act
        var result = saleItem.ApplyDiscount();
        
        // Assert
        Assert.Equal(0m, result);
    }
}