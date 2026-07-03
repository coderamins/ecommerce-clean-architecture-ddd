
namespace Ecommerce.Application.DTOs
{
    public record CreateOrderDto(
            List<CreateOrderItemDto> Items
        );
}
