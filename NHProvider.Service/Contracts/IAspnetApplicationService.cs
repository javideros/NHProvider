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
    public interface IAspnetApplicationService 
    {

        AspnetApplication CreateOrLoadApplication(string applicationName);

        void DeleteApplication(AspnetApplication app);

        AspnetApplication GetApplication(System.Guid guid);

        void UpdateApplication(AspnetApplication application);
    }
}