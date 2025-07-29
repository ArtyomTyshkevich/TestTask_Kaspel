
namespace TestTask.Application.Interfaces.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Books { get; }
        IOrderRepository Orders { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
