using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);
        Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
