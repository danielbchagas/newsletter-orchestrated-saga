using Ambev.DeveloperEvaluation.Domain.Entities;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository
{
    /// <summary>
    /// Retrieves a sale
    /// </summary>
    /// <param name="expression">Expression to filter the sale. Using Expression provides flexibility in the search.</param>
    /// <returns></returns>
    Task<Sale?> GetAsync(Expression<Func<Sale, bool>> expression, CancellationToken cancellationToken = default);
    /// <summary>
    /// Creates a new sale
    /// </summary>
    /// <param name="sale"></param>
    /// <returns></returns>
    Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);
    /// <summary>
    /// Updates a sale
    /// </summary>
    /// <param name="sale"></param>
    /// <returns></returns>
    Task<Sale?> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);
    /// <summary>
    /// Deletes a sale
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
