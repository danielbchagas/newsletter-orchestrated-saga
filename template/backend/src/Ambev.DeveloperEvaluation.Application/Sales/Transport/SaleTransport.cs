namespace Ambev.DeveloperEvaluation.Application.Sales.Transport;

public record SaleTransport
{
    public required Guid Id { get; init; }
    public required string SaleNumber { get; init; }
    public required DateTime Date { get; init; }
    public required string CustomerId { get; init; }
    public required string CustomerName { get; init; }
    public required string BranchId { get; init; }
    public required string BranchName { get; init; }
    public bool IsCancelled { get; init; }

    public required IEnumerable<SaleItemTransport> Items { get; init; }
}