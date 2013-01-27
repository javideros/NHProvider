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
    public class AspnetMembershipDao : HibernateDao, IAspnetMembershipDao
    {
        [Transaction(ReadOnly = true)]
        public AspnetMembership GetMembership(Guid userId, AspnetApplication application)
        {
            AspnetMembership result;

            IQuery query = CurrentSession.CreateQuery("from AspnetMembership as member where member.AspnetApplication.ApplicationId = :appId and member.MembershipId = : userId");
            query.SetParameter("appId", application.ApplicationId);
            query.SetParameter("userId", userId);
             result = query.UniqueResult<AspnetMembership>();
            return result;
        }

        [Transaction(ReadOnly = true)]
        public IList<AspnetMembership> GetMembershipByAppIdAndUserName(AspnetApplication application, string username)
        {
            IList<AspnetMembership> results;

            IQuery query = CurrentSession.CreateQuery("from AspnetMembership as member where member.AspnetApplication.ApplicationId = :appId and member.User.UserName = : userName");
            query.SetParameter("appId", application.ApplicationId);
            query.SetString("userName", username);
            results = query.List<AspnetMembership>();
            return results;
        }

        [Transaction(ReadOnly = true)]
        public IList<AspnetMembership> GetMembershipsByAppIdAndUserEmail(AspnetApplication application, string email)
        {
            IList<AspnetMembership> results;

            IQuery query = CurrentSession.CreateQuery("from AspnetMembership as member where member.AspnetApplication.ApplicationId = :appId and member.Email = : email");
            query.SetParameter("appId", application.ApplicationId);
            query.SetString("email", email);
            results = query.List<AspnetMembership>();
            return results;
        }

        [Transaction(ReadOnly = true)]
        public AspnetMembership GetById(Guid id)
        {
            return CurrentSession.Get<AspnetMembership>(id);
        }

        [Transaction(ReadOnly = true)]
        public IList<AspnetMembership> GetAll()
        {
            return GetAll<AspnetMembership>();
        }

        [Transaction]
        public Guid Save(AspnetMembership membership)
        {
            return (Guid)CurrentSession.Save(membership);
        }

        [Transaction]
        public void Update(AspnetMembership membership)
        {
            CurrentSession.Update(membership);
        }

        [Transaction]
        public void Delete(AspnetMembership membership)
        {
            CurrentSession.Delete(membership);
        }

        [Transaction(ReadOnly = true)]
        public AspnetMembership GetMembership(string userName)
        {
            // Assume we were unable to find the user.
            AspnetMembership user = null;

            // Get the user record from the data store.
            try
            {
                IList<AspnetMembership> users =
                    CurrentSession.CreateCriteria(typeof(AspnetMembership)).
                    Add(Expression.Eq("UserName", userName.ToLower())).List<AspnetMembership>();

                if (1 == users.Count)
                {
                    user = users[0] as AspnetMembership;
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
        public AspnetMembership GetAspnetMembership(string username, AspnetApplication application)
        {
            AspnetMembership result;
            IQuery query = CurrentSession.CreateQuery("from AspnetMembership as m where m.AspnetApplication.ApplicationId = :appId and m.UserName = :username");
            query.SetParameter("appId", application.ApplicationId);
            query.SetString("username", username);
            result = query.UniqueResult<AspnetMembership>();
            return result;
        }

        [Transaction(ReadOnly = true)]
        public IList<AspnetMembership> GetAllApplicationUsers(string applicationName, int pageIndex, int pageSize, out int totalRecords)
        {
            IList<AspnetMembership> results;

            IQuery query = CurrentSession.CreateQuery("from AspnetMembership as m where m.AspnetApplication.ApplicationName = :applicationName");
            query.SetString("applicationName", applicationName);
            query.SetFirstResult(pageSize * pageIndex);
            query.SetMaxResults(pageSize);
            results = query.List<AspnetMembership>();

            IQuery query2 = CurrentSession.CreateQuery("from AspnetMembership as m where m.AspnetApplication.ApplicationName = :applicationName");
            query2.SetString("applicationName", applicationName);
            totalRecords = query2.List<AspnetMembership>().Count;

            return results;
        }
    }
}
