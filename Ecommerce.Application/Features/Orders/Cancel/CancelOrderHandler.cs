using Ecommerce.Application.Common.Exceptions;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Orders.Repositories;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Cancel
{
    public sealed class CancelOrderHandler
        :IRequestHandler<CancelOrderCommand,Unit>
    {
        private readonly IOrderRepository _repository;

        public CancelOrderHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CancelOrderCommand command,CancellationToken ct)
        {
            var order= await _repository.GetById(command.OrderId);

            if (order is null)
            {
                throw new NotFoundException("Order not found");
            }

            var result = order.Cancel();
            if(!result.IsSuccess)
            {
                throw new DomainException(result.Error!);
            }

            await _repository.Update(order);

            return Unit.Value;
        }
    }
}
