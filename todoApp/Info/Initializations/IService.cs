using System;
using System.Threading;
using System.Threading.Tasks;
using Info.Data.BaseEntity;

namespace Info.Initializations
{
    public interface IService<TEntity> where TEntity : IBaseEntity
    {
        Task<TDto> FindAsync<TDto>(object[] keyValues, CancellationToken cancellationToken = default);
        Task<TDto> FindAsync<TDto, TKey>(TKey keyValue, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(object[] keyValues, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default);
        void Attach(TEntity item);
        void Detach(TEntity item);
        void Insert(TEntity item);
        void Update(TEntity item);
        void Delete(TEntity item);
        Task<bool> DeleteAsync(object[] keyValues, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default);
        //IQueryable<TDto> Queryable<TDto>();
        //IQueryable<TEntity> Queryable();
        //IQueryable<TDto> QueryableSql<TDto>(string sql, params object[] parameters);
        //IQueryable<TEntity> QueryableSql(string sql, params object[] parameters);
        //void UpdateDto<TDto>(TEntity entity, TDto dto) where TDto : BaseDto;
    }
}
