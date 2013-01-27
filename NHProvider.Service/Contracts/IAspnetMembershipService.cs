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
    public interface IAspnetMembershipService 
    {
        int UpdateMembership(AspnetMembership membership);

        AspnetMembership GetMembership(System.Guid guid, AspnetApplication application);

        AspnetMembership GetMembership(System.Guid guid);

        AspnetMembership GetMembership(string userName);

        int SaveMembership(AspnetMembership user);

        IList<AspnetMembership> GetMembershipByAppIdAndUserName(AspnetApplication Application, string userName);

        IList<AspnetMembership> GetMembershipsByAppIdAndUserEmail(AspnetApplication application, string email);

        AspnetMembership GetAspnetMembership(string username, AspnetApplication application);

        IList<AspnetMembership> GetAllApplicationUsers(string applicationName, int pageIndex, int pageSize, out int totalRecords);
    }
}