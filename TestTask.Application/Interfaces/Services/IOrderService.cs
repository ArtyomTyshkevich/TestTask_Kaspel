using TestTask.Application.DTOs;

namespace TestTask.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderDTO> CreateAsync(OrderRequest request, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<List<OrderDTO>> GetAllAsync(CancellationToken cancellationToken);
        Task<OrderDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<OrderDTO>> GetFilteredAsync(Guid? id, DateTime? createdDate, CancellationToken cancellationToken);
    }
}