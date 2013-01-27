#region Imports

using NHibernate.Type;
using System.Collections.Generic;
using nhprovider.model;
using System;

#endregion

namespace nhprovider.service.contracts
{
    /// <summary>
    /// AspnetRole related Service operations interface.
    /// </summary>
    public interface IAspnetRoleService 
    {
        int SaveAspnetRole(AspnetRole aspRole);

        void DeleteRole(AspnetRole aspRole);

        AspnetRole GetAspnetRole(string username);

        int UpdateAspnetRole(AspnetRole user);

        IList<string> GetRoleNames(AspnetApplication application);

        IList<AspnetRole> GetAspnetRoles(string roleName, Guid applicationId);
    }
}