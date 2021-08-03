using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Info.Data.BaseEntity;

namespace Info.Repository
{
    public interface IRepository<TEntity> where TEntity : IBaseEntity
    {

        Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken = default);
        Task<TEntity> FindAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(object[] keyValues, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default);
        void Attach(TEntity item);
        void Detach(TEntity item);
        void Insert(TEntity item);
        void Delete(TEntity item);
        Task<bool> DeleteAsync(object[] keyValues, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default);

        IQueryable<TEntity> QueryableActive();
    }
}
