namespace Ambev.DeveloperEvaluation.Application.Sales.Transport;

public record SaleItemTransport
{
    public Guid Id { get; init; }
    public required string ProductId { get; init; }
    public required string ProductName { get; init; }
    public required int Quantity { get; init; }
    public required decimal UnitPrice { get; init; }
    public required decimal Discount { get; init; }
}