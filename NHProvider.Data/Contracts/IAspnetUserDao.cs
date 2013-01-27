#region Imports

using nhprovider.model;
using System;
using System.Collections.Generic;

#endregion

namespace nhprovider.data.contracts
{
    /// <summary>
    /// Application related DAO operations interface.
    /// </summary>
    public interface IAspnetUserDao : IDao<AspnetUser, Guid>, ISupportsDeleteDao<AspnetUser>, ISupportsSave<AspnetUser, Guid>
    {
        int DeletebyName(string username);

        AspnetUser GetAspnetUser(string username);

        IList<AspnetUser> GetUsersById(Guid guid);

        IList<AspnetUser> GetUsersByName(string username);

        int GetUsersOnline(AspnetApplication application, DateTime compareTime);

        AspnetUser GetAspnetUser(string username, AspnetApplication application);

        IList<AspnetUser> GetUsersByEmail(string email, string applicationName);

        void RemoveAppUserReferences(AspnetApplication application, string username);
    }
}