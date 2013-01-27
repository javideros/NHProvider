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
    public class AspnetUserDao : HibernateDao, IAspnetUserDao
    {
        [Transaction]
        public int DeletebyName(string username)
        {
           object[] values = new object[] { username };
           IType[] types = new IType[] { NHibernateUtil.String };
           return CurrentSession.Delete("from AspnetUser as u where u.UserName = ?", values, types);
        }

        [Transaction(ReadOnly = true)]
        public AspnetUser GetAspnetUser(string username)
        {
            // Assume we were unable to find the user.
            AspnetUser user = null;

            // Get the user record from the data store.
            try
            {
                IList<AspnetUser> users =
                    CurrentSession.CreateCriteria(typeof(AspnetUser)).
                    Add(Expression.Eq("UserName", username.ToLower())).List<AspnetUser>();

                if (1 == users.Count)
                {
                    user = users[0] as AspnetUser;
                }
                else if (1 < users.Count)
                {
                    throw ExceptionUtil.NewProviderException(Resources.User_TooManyMatching);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionUtil.NewProviderException(Resources.User_UnableToGet, ex);
            }

            // Return the resulting user.
            return user;
        }

        [Transaction(ReadOnly = true)]
        public IList<AspnetUser> GetUsersById(Guid guid)
        {
            IList<AspnetUser> users = CurrentSession.CreateCriteria(typeof(AspnetUser)).
            Add(Expression.Eq("UserId", guid)).List<AspnetUser>();
            return users;
        }

        [Transaction(ReadOnly = true)]
        public IList<AspnetUser> GetUsersByName(string username)
        {
            IList<AspnetUser> users = CurrentSession.CreateCriteria(typeof(AspnetUser)).
                Add(Expression.Eq("UserName", username.ToLower())).List<AspnetUser>();
            return users;
        }

        [Transaction(ReadOnly = true)]
        public int GetUsersOnline(AspnetApplication application, DateTime compareTime)
        {
            IList<AspnetUser> results;
            IQuery query = CurrentSession.CreateQuery("from AspnetUser as aspUser where aspUser.Application.ApplicationId = :appId and aspUser.LastActivityDate > :lastDate");
            query.SetParameter("appId", application.ApplicationId);
            query.SetDateTime("lastDate", compareTime);
            results = query.List<AspnetUser>();
            return results.Count;
        }

        [Transaction(ReadOnly = true)]
        public AspnetUser GetAspnetUser(string username, AspnetApplication application)
        {
            AspnetUser result;
            IQuery query = CurrentSession.CreateQuery("from AspnetUser as aspUser where aspUser.Application.ApplicationId = :appId and aspUser.UserName = :username");
            query.SetParameter("appId", application.ApplicationId);
            query.SetString("username", username);
            result = query.UniqueResult<AspnetUser>();
            return result;
        }

        [Transaction(ReadOnly = true)]
        public IList<AspnetUser> GetUsersByEmail(string email, string applicationName)
        {
            IList<AspnetUser> results;

            IQuery query = CurrentSession.CreateQuery("select m.User from AspnetMembership as m where m.Application.ApplicationName = :applicationName and m.Email = : email");
            query.SetString("applicationName", applicationName);
            query.SetString("email", email);
            results = query.List<AspnetUser>();
            return results;
        }

        [Transaction]
        public void RemoveAppUserReferences(AspnetApplication application, string username)
        {
            object[] values = new object[] { application.ApplicationId, username };
            IType[] types = new IType[] { NHibernateUtil.Guid, NHibernateUtil.String };
            CurrentSession.Delete("from AspnetUser as u where where u.Application.ApplicationId = ? and u.UserName = ?", values, types);
        }

        [Transaction(ReadOnly = true)]
        public IList<AspnetUser> GetAll()
        {
            return GetAll<AspnetUser>();
        }

        [Transaction(ReadOnly = true)]
        public AspnetUser GetById(Guid Id)
        {
            return CurrentSession.Get<AspnetUser>(Id);
        }

        [Transaction]
        public void Delete(AspnetUser entity)
        {
            CurrentSession.Delete(entity);
        }

        public Guid Save(AspnetUser entity)
        {
            return (Guid)CurrentSession.Save(entity);
        }

        public void Update(AspnetUser entity)
        {
            CurrentSession.Update(entity);
        }
    }
}
