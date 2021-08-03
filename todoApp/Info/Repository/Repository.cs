using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Info.Data.BaseEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Info.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private readonly IServiceProvider _serviceProvider;

        protected Repository(DbContext context, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        private DbContext Context { get; }
        protected DbSet<TEntity> DbSet { get; }

        private IMapper _mapper;
        protected IMapper Mapper => _mapper ??= _serviceProvider.GetService<IMapper>();

        public virtual async Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken = default) => await DbSet.FindAsync(keyValues, cancellationToken);

        public virtual async Task<TEntity> FindAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default) => await FindAsync(new object[] { keyValue }, cancellationToken);

        public virtual async Task<bool> ExistsAsync(object[] keyValues, CancellationToken cancellationToken = default)
        {
            var item = await FindAsync(keyValues, cancellationToken);
            return item != null;
        }

        public virtual async Task<bool> ExistsAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default) => await ExistsAsync(new object[] { keyValue }, cancellationToken);

        public virtual void Attach(TEntity item) => DbSet.Attach(item);

        public virtual void Detach(TEntity item) => Context.Entry(item).State = EntityState.Detached;

        public virtual void Insert(TEntity item) => Context.Entry(item).State = EntityState.Added;

        public virtual void Delete(TEntity item) => Context.Entry(item).State = EntityState.Deleted;

        public virtual async Task<bool> DeleteAsync(object[] keyValues, CancellationToken cancellationToken = default)
        {
            foreach (var keyValue in keyValues)
            {
                var item = await FindAsync(keyValue, cancellationToken);
                if (item == null) return false;
                Context.Entry(item).State = EntityState.Deleted;
            }
            return true;
        }

        public virtual async Task<bool> DeleteAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default) => await DeleteAsync(new object[] { keyValue }, cancellationToken);


        public IQueryable<TEntity> QueryableActive()
        {
            return DbSet.Where(dr => dr.Deleted == 0);
        }

    }
}
