using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    public Sale() { }

    public Sale(string saleNumber, DateTime date, string customerId, string customerName, string branchId, string branchName, bool isCancelled, IList<SaleItem> items)
    {
        SaleNumber = saleNumber;
        Date = date;
        CustomerId = customerId;
        CustomerName = customerName;
        BranchId = branchId;
        BranchName = branchName;
        IsCancelled = isCancelled;
        Items = items ?? new List<SaleItem>();
    }

    public string SaleNumber { get; private set; }
    public DateTime Date { get; private set; }
    public string CustomerId { get; private set; }
    public string CustomerName { get; private set; }
    public string BranchId { get; private set; }
    public string BranchName { get; private set; }
    public bool IsCancelled { get; private set; }

    public ICollection<SaleItem> Items { get; private set; }
    public decimal TotalAmount { get; private set; }

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
    
    public void ApplyDiscount() 
        => TotalAmount = Items.Sum(s => s.ApplyDiscount());

    public void CancelSale() 
        => IsCancelled = true;
}
