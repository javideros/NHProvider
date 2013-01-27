using System.Collections.Generic;
using NHibernate.Impl;
using NHibernate.Criterion;

namespace nhprovider.data.contracts
{
    /// <summary>
    /// Generic DAO interface with minimum retrieval methods.
    /// </summary>
    /// <typeparam name="TEntity">Entity to operate with.</typeparam>
    /// <typeparam name="TId">Entity id type.</typeparam>
    public interface IDao<TEntity, TId>
    {
        IList<TEntity> GetAll();
        TEntity GetById(TId Id);
    }
}