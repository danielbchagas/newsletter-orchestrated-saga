using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoFixture;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.Sales;

public class SaleTests
{
    [Fact]
    public void AddItem_AddsItemToSale()
    {
        // Arrange
        var sale = new Fixture().Create<Sale>();
        var item = new Fixture()
            .Build<SaleItem>()
            .With(si => si.Quantity, 1)
            .With(si => si.UnitPrice, 100m)
            .Create();
        
        // Act
        sale.AddItem(item);
        
        // Assert
        Assert.Contains(item, sale.Items);
    }

    [Fact]
    public void RemoveItem_RemovesItemFromSale()
    {
        // Arrange
        var sale = new Fixture().Create<Sale>();
        var item = new Fixture()
            .Build<SaleItem>()
            .With(si => si.Quantity, 1)
            .With(si => si.UnitPrice, 100m)
            .Create();
        
        // Act
        sale.AddItem(item);
        sale.RemoveItem(item);
        
        // Assert
        Assert.DoesNotContain(item, sale.Items);
    }

    [Fact]
    public void ApplyDiscount_CalculatesTotalAmountCorrectly()
    {
        // Arrange
        var item1 = new Fixture()
            .Build<SaleItem>()
            .With(si => si.Quantity, 4)
            .With(si => si.UnitPrice, 100m)
            .Create();
        
        var item2 = new Fixture()
            .Build<SaleItem>()
            .With(si => si.Quantity, 2)
            .With(si => si.UnitPrice, 50m)
            .Create();

        var items = new List<SaleItem> { item1, item2 };
        
        var sale = new Fixture()
            .Build<Sale>()
            .With(s => s.Items, items)
            .Create();
        
        // Act
        sale.ApplyDiscount();
        
        // Assert
        Assert.Equal(460m, sale.TotalAmount);
    }

    [Fact]
    public void CancelSale_SetsIsCancelledToTrue()
    {
        // Arrange
        var sale = new Sale();
        
        // Act
        sale.CancelSale();
        
        // Assert
        Assert.True(sale.IsCancelled);
    }

    [Fact]
    public void AddItem_ThrowsArgumentNullException_WhenItemIsNull()
    {
        // Arrange
        var sale = new Sale();
        
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => sale.AddItem(null));
    }

    [Fact]
    public void RemoveItem_ThrowsArgumentNullException_WhenItemIsNull()
    {
        // Arrange
        var sale = new Sale();
        
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => sale.RemoveItem(null));
    }
}