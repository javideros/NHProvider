#region Imports

using NHibernate.Type;
using System.Collections.Generic;
using nhprovider.model;

#endregion

namespace nhprovider.service.contracts
{
    /// <summary>
    /// AspnetUser related Service operations interface.
    /// </summary>
    public interface IAspnetUserService 
    {
        int SaveAspnetUser(AspnetUser aspUser);

        void RemoveAppUserReferences(AspnetApplication application, string username);

        int DeleteUser(string username);

        AspnetUser GetAspnetUser(string username);

        int UpdateAspnetUser(AspnetUser user);

        IList<AspnetUser> GetUsersById(System.Guid guid);

        IList<AspnetUser> GetUsersByName(string p);

        int GetUsersOnline(AspnetApplication application, System.DateTime compareTime);

        AspnetUser GetAspnetUser(string username, AspnetApplication application);

        IList<AspnetUser> GetUsersByEmail(string email, string applicationName);
    }
}