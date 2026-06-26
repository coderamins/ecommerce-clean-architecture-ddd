using Ecommerce.Application.Commands.CreateOrder;
using Ecommerce.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromServices] CreateOrderHandler handler,
            [FromBody] CreateOrderDto dto)
        {
            var id = await handler.Execute(
                    new CreateOrderCommand(dto)
                );

            return Ok(new { id });
        }
    }
}
