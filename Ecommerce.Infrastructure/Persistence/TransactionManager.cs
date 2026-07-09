using Ecommerce.Application.Common.Interfaces;

namespace Ecommerce.Infrastructure.Persistence
{
    internal class TransactionManager : ITransactionManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;
        public TransactionManager(IUnitOfWork unitOfWork, 
            ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }

        public async Task<T> ExecuteAsync<T>(
          Func<Task<T>> action,
          CancellationToken cancellationToken = default)
        {
            await using var transaction =
                await _db.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var result = await action();

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return result;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }
        }

        public async Task ExecuteAsync(
            Func<Task> action,
            CancellationToken cancellationToken = default)
        {
            await using var transaction =
                await _db.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                await action();

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }
        }
    }
}
