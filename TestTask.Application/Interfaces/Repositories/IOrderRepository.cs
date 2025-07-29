
using TestTask.Domain.Entities;

namespace TestTask.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Order>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<Order>> GetFilteredAsync(Guid? id, DateTime? createdDate, CancellationToken cancellationToken);
        Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateAsync(Order order, CancellationToken cancellationToken);
    }
}