
namespace TestTask.Application.DTOs
{
    public class OrderRequest
    {
        public List<Guid> BooksIds { get; set; } = new List<Guid>();
    }
}
