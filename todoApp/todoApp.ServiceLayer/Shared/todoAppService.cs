using System;
using Info.Data.BaseEntity;
using todoApp.Initializations;

namespace todoApp.ServiceLayer.Shared
{
    public abstract class todoAppService<T, TRepository> : Service<T, TRepository> where T : IBaseEntity
    {
        //public AuthContext AuthContext;
        public todoAppService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            //AuthContext = serviceProvider.GetRequiredService<AuthContext>();
        }

        //public User GetUser()
        //{
        //    return new User { Id = AuthContext.UserId, CompanyId = AuthContext.CompanyId };
        //}

        //public Guid GetSignedUserId()
        //{
        //    return AuthContext.UserId;
        //}

    }
}
