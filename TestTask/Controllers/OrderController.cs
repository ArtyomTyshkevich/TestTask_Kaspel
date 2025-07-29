using Microsoft.AspNetCore.Mvc;
using TestTask.Application.DTOs;
using TestTask.Application.Interfaces.Services;

namespace TestTask.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderDTO>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetByIdAsync(id, cancellationToken);
            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDTO>>> GetAsync([FromQuery] Guid? id, [FromQuery] DateTime? createdDate, CancellationToken cancellationToken)
        {
            var ordersDTO = await _orderService.GetFilteredAsync(id, createdDate, cancellationToken);
            return Ok(ordersDTO);
        }

        [HttpPost]
        public async Task<ActionResult> CreateByBookIds(OrderRequest orderRequest, CancellationToken cancellationToken)
        {
            var orderDTO = await _orderService.CreateAsync(orderRequest, cancellationToken);
            return Ok(orderDTO);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _orderService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
