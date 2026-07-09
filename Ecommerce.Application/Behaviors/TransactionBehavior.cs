using Ecommerce.Application.Common.Abstractions;
using Ecommerce.Application.Common.Interfaces;
using MediatR;

namespace Ecommerce.Application.Behaviors
{
    public sealed class TransactionBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        private readonly ITransactionManager _transactionManager;

        public TransactionBehavior(ITransactionManager transactionManager)
        {
            _transactionManager = transactionManager;
        }

        public Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            return _transactionManager.ExecuteAsync(
                () => next(),
                cancellationToken);
        }
    }
}
