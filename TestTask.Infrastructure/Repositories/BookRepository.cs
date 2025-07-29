using TestTask.Domain.Entities;
using TestTask.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using TestTask.Application.Interfaces.Repositories;

namespace TestTask.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly TestTaskDbContext _context;

        public BookRepository(TestTaskDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Books.ToListAsync(cancellationToken);
        }

        public async Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        }

        public async Task<List<Book>> GetFilteredAsync(string? name, DateTime? releaseDate, CancellationToken cancellationToken)
        {
            var sortedBooks = _context.Books
                .AsQueryable();

            var nameisGiven = name != null;
            if (nameisGiven)
            {
                sortedBooks = sortedBooks.Where(books =>books.Name==name);
            }

            var dateIsGiven = releaseDate.HasValue;
            if (dateIsGiven)
            {
                var date = releaseDate.Value.Date;
                sortedBooks = sortedBooks.Where(books => books.ReleaseDate.Date == date);
            }

            return await sortedBooks.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Book book, CancellationToken cancellationToken)
        {
            await _context.Books.AddAsync(book, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Book book, CancellationToken cancellationToken)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Book>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
        {
            return await _context.Books
                .Where(b => ids.Contains(b.Id))
                .ToListAsync(cancellationToken);
        }
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}