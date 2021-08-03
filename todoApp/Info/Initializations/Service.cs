using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Info.Data.BaseEntity;
using Info.Initializations;
using Info.Repository;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using todoApp.Data.UnitOfWork;

namespace todoApp.Initializations
{
    public abstract class Service<T, TRepository> : IService<T> where T : IBaseEntity
    {
        private readonly IServiceProvider _serviceProvider;

        protected Service(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private IDistributedCachingService _distributedCachingService;
        private IDistributedCachingService DistributedCachingService => _distributedCachingService ??= _serviceProvider.GetService<IDistributedCachingService>();


        private IMapper _mapper;
        protected IMapper Mapper => _mapper ??= _serviceProvider.GetService<IMapper>();


        private IMemoryCache _memoryCache;
        protected IMemoryCache MemoryCache => _memoryCache ??= _serviceProvider.GetService<IMemoryCache>();


        private IRepository<T> _repository;

        private TRepository _tRepository;

        protected TRepository Repository
        {
            get
            {
                _repository ??= _serviceProvider.GetService<IRepository<T>>();
                return _tRepository ??= (TRepository)_repository;
            }
        }

        private IOptions<ApplicationInfoOptions> _applicationInfoOptions;
        protected IOptions<ApplicationInfoOptions> ApplicationInfoOptions => _applicationInfoOptions ??= _serviceProvider.GetService<IOptions<ApplicationInfoOptions>>();

        private IUnitOfWork _unitOfWork;
        protected IUnitOfWork UnitOfWork => _unitOfWork ??= _serviceProvider.GetService<IUnitOfWork>();


        private IHttpContextAccessor _contextAccessor;
        private IHttpContextAccessor ContextAccessor => _contextAccessor ??= _serviceProvider.GetService<IHttpContextAccessor>();

        public virtual void Attach(T item) => _repository.Attach(item);

        public virtual void Delete(T item) => _repository.Delete(item);

        public virtual async Task<bool> DeleteAsync(object[] keyValues, CancellationToken cancellationToken = default) => await _repository.DeleteAsync(keyValues, cancellationToken);

        public virtual async Task<bool> DeleteAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default) => await _repository.DeleteAsync(keyValue, cancellationToken);

        public virtual void Detach(T item) => _repository.Detach(item);

        public virtual async Task<bool> ExistsAsync(object[] keyValues, CancellationToken cancellationToken = default) => await _repository.ExistsAsync(keyValues, cancellationToken);

        public virtual async Task<bool> ExistsAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default) => await _repository.ExistsAsync(keyValue, cancellationToken);

        public virtual async Task<TDto> FindAsync<TDto>(object[] keyValues, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.FindAsync(keyValues, cancellationToken);
            return Mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto> FindAsync<TDto, TKey>(TKey keyValue, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.FindAsync(keyValue, cancellationToken);
            return Mapper.Map<TDto>(entity);
        }
        public virtual void Insert(T item)
        {
            item.CreatedDate = DateTime.Now;
            item.LastUpdatedDate = DateTime.Now;
            _repository.Insert(item);
        }

        public virtual void Update(T item)
        {
            //item.LastUpdatedDate = DateTime.Now;
        }

        //protected bool TryGetValueFromRedis<TItem>(object key, out TItem value)
        //{
        //    value = DistributedCachingService.Get<TItem>(JsonConvert.SerializeObject(key));
        //    return value != null;
        //}

        //protected void SetValueToRedis(object key, object value, CachingEntryOptions cacheExpOptions)
        //{
        //    DistributedCachingService.Set(JsonConvert.SerializeObject(key), value, cacheExpOptions);
        //}

        //public virtual void UpdateDto<TDto>(T entity, TDto dto) where TDto : BaseDto
        //{
        //    Mapper.Map(dto, entity);
        //}

        //protected PagedDto<TDto> Paging<TEntity, TDto, TQ>(IQueryable<TEntity> categoryQuery, Pagination pagination, Sort sort, Query<TQ> query) where TQ : class
        //{
        //    var modelBuilder = new ODataConventionModelBuilder();
        //    modelBuilder.AddEntityType(typeof(TEntity));
        //    var edmModel = modelBuilder.GetEdmModel();
        //    var oDataQueryContext = new ODataQueryContext(edmModel, typeof(TEntity), new ODataPath());
        //    var queryDictionary = new Dictionary<string, StringValues>();

        //    var filterValue = "1 eq 1";

        //    if (!string.IsNullOrEmpty(query?.generalSearch))
        //    {
        //        var generalSearchSplit = Regex.Matches(query.generalSearch, @"[\""].+?[\""]|[^ ]+")
        //                                                .Select(m => m.Value)
        //                                                .ToList();

        //        filterValue += "and (1 eq 2";
        //        var properties = typeof(TDto).GetProperties();
        //        foreach (var item in generalSearchSplit)
        //        {
        //            var containStr = item.Replace("\"", "").Trim();
        //            if (containStr.Length <= 1) continue;

        //            foreach (var property in properties)
        //            {
        //                if (property.PropertyType == typeof(int))
        //                {
        //                    filterValue += " or contains(cast(" + property.Name + ", 'Edm.String'), '" + containStr + "')";
        //                }
        //                else if (property.PropertyType == typeof(string))
        //                {
        //                    filterValue += " or contains(" + property.Name + ", '" + containStr + "') ";
        //                }
        //            }
        //        }

        //        filterValue += ")";
        //    }

        //    if (query?.Columns != null)
        //    {

        //        filterValue += "and (1 eq 2";
        //        var properties = typeof(TQ).GetProperties();

        //        foreach (var property in properties)
        //        {
        //            if (property.GetValue(query.Columns) == null) continue;

        //            var generalSearchSplit = Regex.Matches(property.GetValue(query.Columns).ToString(), @"[\""].+?[\""]|[^ ]+")
        //                .Select(m => m.Value)
        //                .ToList();

        //            foreach (var item in generalSearchSplit)
        //            {
        //                var containStr = item.Replace("\"", "").Trim();
        //                if (containStr.Length <= 1) continue;

        //                if (property.PropertyType == typeof(int))
        //                {
        //                    filterValue += " or contains(cast(" + property.Name + ", 'Edm.String'), '" + containStr + "')";
        //                }
        //                else if (property.PropertyType == typeof(string))
        //                {
        //                    filterValue += " or contains(" + property.Name + ", '" + containStr + "') ";
        //                }
        //            }
        //        }

        //        filterValue += ")";
        //    }

        //    queryDictionary.Add("$filter", filterValue);

        //    if (sort?.field != null)
        //    {
        //        var checkField = typeof(TDto).GetProperty(sort.field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        //        if (checkField != null)
        //        {
        //            if (sort.sort == "asc")
        //            {
        //                queryDictionary.Add("$orderby", sort.field);
        //            }
        //            else
        //            {
        //                queryDictionary.Add("$orderby", sort.field + " desc");
        //            }
        //        }
        //    }

        //    queryDictionary.Add("$skip", ((pagination.page - 1) * pagination.perpage).ToString());
        //    queryDictionary.Add("$top", pagination.perpage.ToString());

        //    var httpRequest = ContextAccessor.HttpContext.Request;
        //    httpRequest.Query = new QueryCollection(queryDictionary);
        //    ODataQueryOptions q = new ODataQueryOptions<TEntity>(oDataQueryContext, httpRequest);

        //    PagedDto<TDto> result;
        //    if (typeof(TEntity) == typeof(TDto))
        //    {
        //        result = new PagedDto<TDto>
        //        {
        //            data = (q.ApplyTo(categoryQuery) as IQueryable<TDto>)?.ToList()
        //        };
        //    }
        //    else
        //    {
        //        result = new PagedDto<TDto>
        //        {
        //            data = (q.ApplyTo(categoryQuery) as IQueryable<TEntity>).ProjectTo<TDto>(Mapper.ConfigurationProvider).ToList()
        //        };
        //    }

        //    int? totalCount;
        //    if (!string.IsNullOrEmpty(query?.generalSearch) || query?.Columns != null)
        //    {
        //        totalCount = (q.Filter.ApplyTo(categoryQuery, new ODataQuerySettings()) as IQueryable<TEntity>)?.Count();
        //    }
        //    else
        //    {
        //        totalCount = categoryQuery.Count();
        //    }

        //    result.meta = new PageMeta
        //    {
        //        total = totalCount,
        //        pages = totalCount / pagination.perpage,
        //        page = pagination.page,
        //        perpage = pagination.perpage,
        //        field = sort?.field,
        //        sort = sort?.sort,
        //    };

        //    return result;
        //}
    }
}
