using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using TestTask.Domain.Entities;

namespace TestTask.Infrastructure.Contexts
{
    public class TestTaskDbContext : DbContext
    {
        public TestTaskDbContext(DbContextOptions<TestTaskDbContext> options)
            : base(options)
        {
            if (Database.IsRelational())
            {
                Database.Migrate();
            }
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
