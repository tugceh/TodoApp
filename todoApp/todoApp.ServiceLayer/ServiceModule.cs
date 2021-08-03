using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Info.Initializations;

namespace todoApp.ServiceLayer
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IService<>))).AsImplementedInterfaces();
        }
    }
}
