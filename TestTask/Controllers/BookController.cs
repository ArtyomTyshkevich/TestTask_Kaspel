using Microsoft.AspNetCore.Mvc;
using TestTask.Application.DTOs;
using TestTask.Application.Interfaces.Services;

namespace TestTask.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDTO>>> GetAsync([FromQuery] string? name, [FromQuery] DateTime? releaseDate, CancellationToken cancellationToken)
        {
            var booksDTO = await _bookService.GetFilteredAsync(name, releaseDate, cancellationToken);
            return Ok(booksDTO);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BookDTO>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var book = await _bookService.GetByIdAsync(id, cancellationToken);
            if (book == null)
                return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] BookDTO bookDTO, CancellationToken cancellationToken)
        {
            await _bookService.AddAsync(bookDTO, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = bookDTO.Id }, bookDTO);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] BookDTO bookDTO, CancellationToken cancellationToken)
        {
            if (id != bookDTO.Id)
                return BadRequest("ID в пути и в теле не совпадают");

            await _bookService.UpdateAsync(bookDTO, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _bookService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
