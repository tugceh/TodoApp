using System;
using System.Threading;
using System.Threading.Tasks;
using todoApp.Data;

namespace todoApp.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext Context { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            await Context.Database.BeginTransactionAsync();
        }
        public void CommitTransaction()
        {
            Context.Database.CommitTransaction();
        }
        public void Rollback()
        {
            Context.Database.RollbackTransaction();
        }
        public void Dispose()
        {
            //Context.Database.CurrentTransaction?.Dispose();
        }
    }
}
