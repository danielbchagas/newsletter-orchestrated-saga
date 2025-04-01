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

    public string SaleNumber { get; set; }
    public DateTime Date { get; set; }
    public string CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string BranchId { get; set; }
    public string BranchName { get; set; }
    public bool IsCancelled { get; set; }

    public ICollection<SaleItem> Items { get; set; }
    public decimal TotalAmount { get; set; }

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
