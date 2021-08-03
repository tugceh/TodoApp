using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace todoApp.ServiceLayer
{

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
        }

    }

    public static class AutoMapperConfigs
    {
        public static void AddMapper(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
