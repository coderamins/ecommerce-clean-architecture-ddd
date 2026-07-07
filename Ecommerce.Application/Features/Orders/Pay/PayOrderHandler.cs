using Ecommerce.Domain.Common;
using Ecommerce.Domain.Orders.Repositories;

namespace Ecommerce.Application.Features.Orders.Pay
{
    public class PayOrderHandler
    {
        private readonly IOrderRepository _orderRepo;

        public PayOrderHandler(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public async Task Handle(PayOrderCommand command)
        {
            var order = await _orderRepo.Get(command.orderId);

            if(order is null)
            {
                throw new DomainException("Order not found");
            }

            var result = order.Pay();
            if(!result.IsSuccess)
            {
                throw new DomainException(result.Error!);
            }

            await _orderRepo.Update(order);
        }
    }
}
