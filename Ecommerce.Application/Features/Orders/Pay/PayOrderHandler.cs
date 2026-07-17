using Ecommerce.Application.Common.Exceptions;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Orders.Repositories;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Pay
{
    public sealed class PayOrderHandler:IRequestHandler<PayOrderCommand, Unit>
    {
        private readonly IOrderRepository _orderRepo;

        public PayOrderHandler(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public async Task<Unit> Handle(
            PayOrderCommand command,
            CancellationToken cancellationToken)
        {
            var order = await _orderRepo.Get(command.OrderId);

            if(order is null)
            {
                throw new NotFoundException("Order not found");
            }

            var result = order.Pay();
            if(!result.IsSuccess)
            {
                throw new DomainException(result.Error!);
            }

            await _orderRepo.Update(order);

            return Unit.Value;
        }
    }
}
