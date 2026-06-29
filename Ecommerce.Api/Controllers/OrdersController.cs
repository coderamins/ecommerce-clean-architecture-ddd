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
