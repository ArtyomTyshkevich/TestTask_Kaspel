
namespace TestTask.Application.DTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<BookDTO> Books { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
