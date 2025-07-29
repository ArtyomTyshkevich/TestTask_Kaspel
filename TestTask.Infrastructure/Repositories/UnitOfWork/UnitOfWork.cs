using TestTask.Application.Interfaces.Repositories;
using TestTask.Application.Interfaces.Repositories.UnitOfWork;
using TestTask.Infrastructure.Contexts;

namespace TestTask.Infrastructure.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TestTaskDbContext _dbContext;
        public IBookRepository Books { get; private set; }
        public IOrderRepository Orders { get; private set; }


        public UnitOfWork(
            TestTaskDbContext dbContext)
        {
            _dbContext = dbContext;

            Books = new BookRepository(_dbContext);
            Orders = new OrderRepository(_dbContext);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}