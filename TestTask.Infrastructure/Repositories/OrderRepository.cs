using Microsoft.EntityFrameworkCore;
using TestTask.Application.Interfaces.Repositories;
using TestTask.Domain.Entities;
using TestTask.Infrastructure.Contexts;

namespace TestTask.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TestTaskDbContext _context;

        public OrderRepository(TestTaskDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Orders
                .Include(o => o.Books)
                .ToListAsync(cancellationToken);
        }

        public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Orders
                .Include(o => o.Books)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task<List<Order>> GetFilteredAsync(Guid? id, DateTime? createdDate, CancellationToken cancellationToken)
        {
            var sortedOrder = _context.Orders
                .Include(o => o.Books)
                .AsQueryable();

            var idisGiven = id.HasValue && id != Guid.Empty;
            if (idisGiven)
            {
                sortedOrder = sortedOrder.Where(o => o.Id == id.Value);
            }

            var dateIsGiven = createdDate.HasValue;
            if (dateIsGiven)
            {
                var date = createdDate.Value.Date;
                sortedOrder = sortedOrder.Where(o => o.CreatedDate.Date == date);
            }

            return await sortedOrder.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Order order, CancellationToken cancellationToken)
        {
            await _context.Orders.AddAsync(order, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Order order, CancellationToken cancellationToken)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FindAsync(new object[] { id }, cancellationToken);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
