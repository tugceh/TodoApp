using System;
using Info.Data.BaseEntity;
using Info.Initializations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Info
{
    public abstract class BaseApiController<TController, TEntity, TService> : ControllerBase where TEntity : IBaseEntity where TService : IService<TEntity>
    {
        private readonly IServiceProvider _serviceProvider;


        //private ILogger<TController> _logger;
        //protected ILogger<TController> Logger => _logger ??= _serviceProvider.GetService<ILogger<TController>>();


        private IService<TEntity> _service;

        private TService _tService;

        protected TService Service
        {
            get
            {
                _service ??= _serviceProvider.GetRequiredService<IService<TEntity>>();
                return _tService ??= (TService)_service;
            }
        }

        protected BaseApiController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}
