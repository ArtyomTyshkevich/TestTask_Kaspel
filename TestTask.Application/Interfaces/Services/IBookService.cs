using TestTask.Application.DTOs;

namespace TestTask.Application.Interfaces.Services
{
    public interface IBookService
    {
        Task AddAsync(BookDTO bookDto, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<List<BookDTO>> GetAllAsync(CancellationToken cancellationToken);
        Task<BookDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<BookDTO>> GetFilteredAsync(string? name, DateTime? releaseDate, CancellationToken cancellationToken);
        Task UpdateAsync(BookDTO bookDto, CancellationToken cancellationToken);
    }
}