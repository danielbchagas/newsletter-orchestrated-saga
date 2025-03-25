namespace Ambev.DeveloperEvaluation.Domain.Entities;

class SaleItem
{
    public Guid Id { get; private set; }
    public string ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Discount { get; private set; }
    public decimal Total => (UnitPrice * Quantity) - Discount;
}
