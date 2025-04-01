using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem : BaseEntity
{
    public SaleItem(){}

    public SaleItem(string productId, string productName, int quantity, decimal unitPrice, decimal discount)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
    }

    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }

    public decimal ApplyDiscount()
    {
        if (Quantity >= 4)
            TotalAmount = (UnitPrice * Quantity) * 0.9m;
        else
            TotalAmount = UnitPrice * Quantity;
        
        return TotalAmount;
    }
}
