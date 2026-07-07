using Ecommerce.Application.Features.Orders.Cancel;
using Ecommerce.Application.Features.Orders.Create;
using Ecommerce.Application.Features.Orders.Get;
using Ecommerce.Application.Features.Orders.Pay;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ISender _sender;

        public OrdersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _sender.Send(new GetOrderQuery(id));

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {
            var id = await _sender.Send(command);

            return Created($"/orders/{id}", new { id });
        }

        [HttpPost("{id}/pay")]
        public async Task<IActionResult> Pay(Guid id,
            [FromServices] PayOrderHandler command)
        {
            await _sender.Send(command);

            return NoContent();
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> Cancel(Guid id,
            [FromServices] CancelOrderHandler command)
        {
            await _sender.Send(command);

            return NoContent();
        }
    }
}
