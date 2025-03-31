namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale
{
    public Sale() { }

    public Sale(Guid id, string saleNumber, DateTime date, string customerId, string customerName, string branchId, string branchName, bool isCancelled, IList<SaleItem> items)
    {
        Id = id;
        SaleNumber = saleNumber;
        Date = date;
        CustomerId = customerId;
        CustomerName = customerName;
        BranchId = branchId;
        BranchName = branchName;
        IsCancelled = isCancelled;
        Items = items ?? new List<SaleItem>();
    }

    public Guid Id { get; private set; }
    public string SaleNumber { get; private set; }
    public DateTime Date { get; private set; }
    public string CustomerId { get; private set; }
    public string CustomerName { get; private set; }
    public string BranchId { get; private set; }
    public string BranchName { get; private set; }
    public bool IsCancelled { get; private set; }

    public IList<SaleItem> Items { get; private set; }
    public decimal TotalAmount => Items.Sum(i => i.Total);

    public void AddItem(SaleItem item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        Items.Add(item);
    }

    public void RemoveItem(SaleItem item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        Items.Remove(item);
    }

    public void CancelSale() => IsCancelled = true;
}
