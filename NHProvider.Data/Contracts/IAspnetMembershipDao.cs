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
    public interface IAspnetMembershipDao : IDao<AspnetMembership, Guid>, ISupportsDeleteDao<AspnetMembership>, ISupportsSave<AspnetMembership, Guid>
    {

        AspnetMembership GetMembership(Guid guid, AspnetApplication application);

        IList<AspnetMembership> GetMembershipByAppIdAndUserName(AspnetApplication application, string username);

        IList<AspnetMembership> GetMembershipsByAppIdAndUserEmail(AspnetApplication application, string email);

        AspnetMembership GetMembership(string userName);

        AspnetMembership GetAspnetMembership(string username, AspnetApplication application);

        IList<AspnetMembership> GetAllApplicationUsers(string applicationName, int pageIndex, int pageSize, out int totalRecords);
    }
}