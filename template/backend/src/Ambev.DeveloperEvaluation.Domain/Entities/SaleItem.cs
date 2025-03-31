namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem
{
    public SaleItem(){}

    public SaleItem(Guid id, string productId, string productName, int quantity, decimal unitPrice, decimal discount)
    {
        Id = id;
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
    }

    public Guid Id { get; private set; }
    public string ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Discount { get; private set; }
    public decimal Total => (UnitPrice * Quantity) - Discount;
}
