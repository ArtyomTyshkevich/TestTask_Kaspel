using TestTask.Domain.Entities;

namespace TestTask.Application.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task AddAsync(Book book, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Book>> GetAllAsync(CancellationToken cancellationToken);
        Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Book>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
        Task<List<Book>> GetFilteredAsync(string? name, DateTime? ReleaseDate, CancellationToken cancellationToken);
        Task UpdateAsync(Book book, CancellationToken cancellationToken);
    }
}