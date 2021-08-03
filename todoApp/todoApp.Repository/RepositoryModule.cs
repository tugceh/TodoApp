using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Info.Repository;

namespace todoApp.Repository
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IRepository<>))).AsImplementedInterfaces();
        }
    }
}
