#region Imports

using nhprovider.model;
using System;
using System.Collections.Generic;

#endregion

namespace nhprovider.data.contracts
{
    /// <summary>
    /// Role related DAO operations interface.
    /// </summary>
    public interface IAspnetRoleDao : IDao<AspnetRole, Guid>, ISupportsDeleteDao<AspnetRole>, ISupportsSave<AspnetRole, Guid>
    {
        AspnetRole GetAspnetRole(string roleName);

        IList<string> GetRoleNames(AspnetApplication application);

        IList<AspnetRole> GetAspnetRoles(string roleName, Guid applicationId);

        void RemoveAppRoleReferences(Guid applicationId, Guid roleId);
    }
}