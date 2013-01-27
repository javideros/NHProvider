#region Imports

using System.Collections;
using System.Collections.Generic;
using Spring.Data.NHibernate.Generic;
using Spring.Transaction.Interceptor;
using System;
using NHibernate;
using NHibernate.Type;
using nhprovider.data.contracts;
using nhprovider.model;
using nhprovider.util;
using NHibernate.Criterion;
using NHProvider.Data.Properties;
using Spring.Stereotype;


#endregion

namespace nhprovider.data.impl
{
    /// <summary>
    /// Data access object for Applications
    /// </summary>
    [Repository]
    public class AspnetApplicationDao : HibernateDao, IAspnetApplicationDao
    {
        [Transaction(ReadOnly = true)]
        public AspnetApplication GetById(Guid id)
        {
            return CurrentSession.Get<AspnetApplication>(id);
        }

        [Transaction(ReadOnly = true)]
        public IList<AspnetApplication> GetAll()
        {
            return GetAll<AspnetApplication>();
        }

        [Transaction]
        public Guid Save(AspnetApplication application)
        {
            return (Guid)CurrentSession.Save(application);
        }

        [Transaction]
        public void Update(AspnetApplication application)
        {
            CurrentSession.Update(application);
        }

        [Transaction]
        public void Delete(AspnetApplication application)
        {
            CurrentSession.Delete(application);
        }

        /// <summary>
        /// Determines whether or not the given application name has already been registered; if not; creates the corresponding
        /// application instance.
        /// </summary>
        /// <param name="appName">name of the application for which to get the details.</param>
        /// <returns><see cref="Application"/> instance representing the given application name.</returns>
        [Transaction]
        public AspnetApplication CreateOrLoadApplication(string appName)
        {
            // Prepare a place-holder for the application.
            AspnetApplication app = GetApplication(appName);

            // Determine if the application record does not exists in the data store.
            if (null == app)
            {
                try
                {
                    // Create a new application instance.
                    app = new AspnetApplication();
                    app.ApplicationName = appName;
                    app.LoweredApplicationName = appName.ToLowerInvariant();
                    // Update it in the data store.
                    Save(app);
                }
                catch (Exception ex)
                {
                    throw ExceptionUtil.NewProviderException(Resources.App_UnableToCreateOrLoad, ex);
                }
            }

            // Return the resulting application instance.
            return app;
        }

        /// <summary>
        /// Gets <see cref="Application"/> instance for the given application name.
        /// </summary>
        /// <param name="appName">name of the application for whom to retrieve information.</param>
        /// <returns><see cref="Application"/> object representing the given application name; otherwise, <c>null</c>.</returns>
        [Transaction(ReadOnly = true)]
        public AspnetApplication GetApplication(string appName)
        {
            // Assume we were unable to find the application.
            AspnetApplication app = null;

            // Get the application record from the data store.
            try
            {
                IList<AspnetApplication> apps =
                    CurrentSession.CreateCriteria(typeof(AspnetApplication)).
                        Add(Expression.Eq("ApplicationName", appName)).List<AspnetApplication>();

                if (1 == apps.Count)
                {
                    app = apps[0] as AspnetApplication;
                }
                else if (1 < apps.Count)
                {
                    throw ExceptionUtil.NewProviderException(Resources.App_TooManyMatching);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionUtil.NewProviderException(Resources.App_UnableToGet, ex);
            }

            // Return the resulting application.
            return app;
        }
        
        [Transaction(ReadOnly = true)]
        public IList<AspnetApplication> Find(string queryString)
        {
            IList<AspnetApplication> results;
            results = CurrentSession.CreateQuery(queryString).List<AspnetApplication>();
            return results;
        }

        [Transaction(ReadOnly = true)]
        public IList<AspnetApplication> Find(string queryString, object value, global::NHibernate.Type.IType type) 
        {
            object[] values = new object[] { value };
            IType[] types = new IType[] { type };
            return Find(queryString, values, types);
        }

        [Transaction(ReadOnly = true)]
        public IList<AspnetApplication> Find(string queryString, object[] values, global::NHibernate.Type.IType[] types)
        {
            IList<AspnetApplication> results;

            IQuery query = CurrentSession.CreateQuery(queryString);
                if ((null != values) && (null != types))
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        query.SetParameter(i, values[i], types[i]);
                    }
                }
                results = query.List<AspnetApplication>();
                return results;
        }
    }
}
