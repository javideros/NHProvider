#region Imports

using nhprovider.model;
using System;

#endregion

namespace nhprovider.data.contracts
{
    /// <summary>
    /// Application related DAO operations interface.
    /// </summary>
    public interface IAspnetApplicationDao : IDao<AspnetApplication, Guid>, ISupportsDeleteDao<AspnetApplication>, ISupportsSave<AspnetApplication, Guid>
    {
        AspnetApplication CreateOrLoadApplication(string appName);
        AspnetApplication GetApplication(string appName);
    }
}