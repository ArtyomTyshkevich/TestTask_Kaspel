
using System.ComponentModel.DataAnnotations;

namespace TestTask.Domain.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
        public decimal TotalPrice { get; set; }
    }
}
