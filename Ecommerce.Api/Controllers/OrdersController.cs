using Ecommerce.Application.Commands.CreateOrder;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Queries.GetOrder;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id, [FromServices] GetOrderHandler handler)
        {
            var result = await handler.Execute(new(id));

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult>
        Create([FromBody] CreateOrderDto dto, [FromServices] CreateOrderHandler handler)
        {
            var id =await handler.Execute(new(dto));

            return Created($"/orders/{id}",
                new
                {
                    id
                });
        }
    }
}
