using System.ComponentModel.DataAnnotations;

namespace TestTask.Domain.Entities
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
