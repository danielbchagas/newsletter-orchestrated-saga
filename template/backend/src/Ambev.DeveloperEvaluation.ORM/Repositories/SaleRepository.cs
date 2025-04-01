using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    public SaleRepository(DefaultContext context) => _context = context;

    public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return sale;
    }

    public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sale = await _context.Sales.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        if (sale is null) return Guid.Empty;

        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync(cancellationToken);

        return sale.Id;
    }

    public async Task<Sale?> GetAsync(Expression<Func<Sale, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await _context
            .Sales
            .Include(x => x.Items)
            .FirstOrDefaultAsync(expression, cancellationToken);
    }

    public async Task<Sale?> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        var exists = await _context.Sales.AnyAsync(s => s.Id == sale.Id, cancellationToken);

        if (!exists) return null;

        _context.Sales.Update(sale);
        await _context.SaveChangesAsync(cancellationToken);

        return sale;
    }
}
