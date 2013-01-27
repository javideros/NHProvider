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
    /// Data access object for Roles
    /// </summary>
    [Repository]
    public class AspnetRoleDao : HibernateDao, IAspnetRoleDao
    {
        [Transaction(ReadOnly = true)]
        public IList<AspnetRole> GetAll()
        {
            return GetAll<AspnetRole>();
        }

        [Transaction(ReadOnly = true)]
        public AspnetRole GetById(Guid Id)
        {
            return CurrentSession.Get<AspnetRole>(Id);
        }

        [Transaction]
        public void Delete(AspnetRole entity)
        {
            CurrentSession.Delete(entity);
        }

        [Transaction]
        public Guid Save(AspnetRole entity)
        {
            return (Guid)CurrentSession.Save(entity);
        }

        [Transaction]
        public void Update(AspnetRole entity)
        {
            CurrentSession.Update(entity);
        }

        [Transaction]
        public AspnetRole GetAspnetRole(string roleName)
        {
            AspnetRole role =
                    CurrentSession.CreateCriteria(typeof(AspnetRole)).
                    Add(Expression.Eq("RoleName", roleName.ToLower())).UniqueResult<AspnetRole>();
            return role;
        }

        [Transaction(ReadOnly = true)]
        public IList<string> GetRoleNames(AspnetApplication application)
        {
            IList<string> results;

            IQuery query = CurrentSession.CreateQuery("select m.RoleName from AspnetRole as m where m.AspnetApplication.ApplicationId = :applicationId");
            query.SetGuid("applicationId", application.ApplicationId);
            results = query.List<string>();
            return results;
        }

        [Transaction(ReadOnly = true)]
        public IList<AspnetRole> GetAspnetRoles(string roleName, Guid applicationId)
        {
            IList<AspnetRole> results;

            IQuery query = CurrentSession.CreateQuery("select m from AspnetRole as m where m.AspnetApplication.ApplicationId = :applicationId");
            query.SetGuid("applicationId", applicationId);
            results = query.List<AspnetRole>();
            return results;
        }

        [Transaction]
        public void RemoveAppRoleReferences(Guid applicationId, Guid roleId)
        {
            object[] values = new object[] { roleId, applicationId };
            IType[] types = new IType[] { NHibernateUtil.Guid, NHibernateUtil.Guid };
            CurrentSession.Delete("from AspnetRole as r where r.RoleId = ? and r.AspnetApplication.ApplicationId = ?", values, types);
        }

        //[Transaction]
        //public int DeletebyName(string username)
        //{
        //   object[] values = new object[] { username };
        //   IType[] types = new IType[] { NHibernateUtil.String };
        //   return CurrentSession.Delete("from AspnetUser as u where u.UserName = ?", values, types);
        //}

        //[Transaction(ReadOnly = true)]
        //public AspnetUser GetAspnetUser(string username)
        //{
        //    // Assume we were unable to find the user.
        //    AspnetUser user = null;

        //    // Get the user record from the data store.
        //    try
        //    {
        //        IList<AspnetUser> users =
        //            CurrentSession.CreateCriteria(typeof(AspnetUser)).
        //            Add(Expression.Eq("UserName", username.ToLower())).List<AspnetUser>();

        //        if (1 == users.Count)
        //        {
        //            user = users[0] as AspnetUser;
        //        }
        //        else if (1 < users.Count)
        //        {
        //            throw ExceptionUtil.NewProviderException(Resources.User_TooManyMatching);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ExceptionUtil.NewProviderException(Resources.User_UnableToGet, ex);
        //    }

        //    // Return the resulting user.
        //    return user;
        //}

        //[Transaction(ReadOnly = true)]
        //public IList<AspnetUser> GetUsersById(Guid guid)
        //{
        //    IList<AspnetUser> users = CurrentSession.CreateCriteria(typeof(AspnetUser)).
        //    Add(Expression.Eq("UserId", guid)).List<AspnetUser>();
        //    return users;
        //}

        //[Transaction(ReadOnly = true)]
        //public IList<AspnetUser> GetUsersByName(string username)
        //{
        //    IList<AspnetUser> users = CurrentSession.CreateCriteria(typeof(AspnetUser)).
        //        Add(Expression.Eq("UserName", username.ToLower())).List<AspnetUser>();
        //    return users;
        //}

        //[Transaction(ReadOnly = true)]
        //public int GetUsersOnline(AspnetApplication application, DateTime compareTime)
        //{
        //    IList<AspnetUser> results;
        //    IQuery query = CurrentSession.CreateQuery("from AspnetUser as aspUser where aspUser.Application.ApplicationId = :appId and aspUser.LastActivityDate > :lastDate");
        //    query.SetParameter("appId", application.ApplicationId);
        //    query.SetDateTime("lastDate", compareTime);
        //    results = query.List<AspnetUser>();
        //    return results.Count;
        //}

        //[Transaction(ReadOnly = true)]
        //public AspnetUser GetAspnetUser(string username, AspnetApplication application)
        //{
        //    AspnetUser result;
        //    IQuery query = CurrentSession.CreateQuery("from AspnetUser as aspUser where aspUser.Application.ApplicationId = :appId and aspUser.UserName = :username");
        //    query.SetParameter("appId", application.ApplicationId);
        //    query.SetString("username", username);
        //    result = query.UniqueResult<AspnetUser>();
        //    return result;
        //}

        //[Transaction(ReadOnly = true)]
        //public IList<AspnetUser> GetUsersByEmail(string email, string applicationName)
        //{
        //    IList<AspnetUser> results;

        //    IQuery query = CurrentSession.CreateQuery("select m.User from AspnetMembership as m where m.Application.ApplicationName = :applicationName and m.Email = : email");
        //    query.SetString("applicationName", applicationName);
        //    query.SetString("email", email);
        //    results = query.List<AspnetUser>();
        //    return results;
        //}

        //[Transaction(ReadOnly = true)]
        //public IList<AspnetUser> GetAllApplicationUsers(string applicationName, int pageIndex, int pageSize, out int totalRecords)
        //{
        //    IList<AspnetUser> results;

        //    IQuery query = CurrentSession.CreateQuery("from AspnetUser as m where m.Application.ApplicationName = :applicationName");
        //    query.SetString("applicationName", applicationName);
        //    query.SetFirstResult(pageSize * pageIndex);
        //    query.SetMaxResults(pageSize);
        //    results = query.List<AspnetUser>();

        //    IQuery query2 = CurrentSession.CreateQuery("from AspnetUser as m where m.Application.ApplicationName = :applicationName");
        //    query2.SetString("applicationName", applicationName);
        //    totalRecords = query2.List<AspnetUser>().Count;

        //    return results;
        //}

        //[Transaction]
        //public void RemoveAppUserReferences(AspnetApplication application, string username)
        //{
        //    object[] values = new object[] { application.ApplicationId, username };
        //    IType[] types = new IType[] { NHibernateUtil.Guid, NHibernateUtil.String };
        //    CurrentSession.Delete("from AspnetUser as u where where u.Application.ApplicationId = ? and u.UserName = ?", values, types);
        //}

        //[Transaction(ReadOnly = true)]
        //public IList<AspnetUser> GetAll()
        //{
        //    return GetAll<AspnetUser>();
        //}

        //[Transaction(ReadOnly = true)]
        //public AspnetUser GetById(Guid Id)
        //{
        //    return CurrentSession.Get<AspnetUser>(Id);
        //}

        //[Transaction]
        //public void Delete(AspnetUser entity)
        //{
        //    CurrentSession.Delete(entity);
        //}

        //public Guid Save(AspnetUser entity)
        //{
        //    return (Guid)CurrentSession.Save(entity);
        //}

        //public void Update(AspnetUser entity)
        //{
        //    CurrentSession.Update(entity);
        //}
    }
}
