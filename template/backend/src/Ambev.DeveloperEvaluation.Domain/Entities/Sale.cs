namespace Ambev.DeveloperEvaluation.Domain.Entities;

class Sale
{
    public Guid Id { get; private set; }
    public string SaleNumber { get; private set; }
    public DateTime Date { get; private set; }
    public string CustomerId { get; private set; }
    public string CustomerName { get; private set; }
    public string BranchId { get; private set; }
    public string BranchName { get; private set; }
    public List<SaleItem> Items { get; private set; }
    public decimal TotalAmount => Items.Sum(i => i.Total);
    public bool IsCancelled { get; private set; }
}
