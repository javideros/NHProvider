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
    public class AspnetUsersInRoleDao : HibernateDao, IAspnetUsersInRoleDao
    {
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

        [Transaction(ReadOnly = true)]
        public IList<AspnetUsersInRole> GetAll()
        {
            return GetAll<AspnetUsersInRole>();
        }

        [Transaction(ReadOnly = true)]
        public AspnetUsersInRole GetById(UserRoleIdentifier Id)
        {
            return CurrentSession.Get<AspnetUsersInRole>(Id);
        }

        [Transaction]
        public void Delete(AspnetUsersInRole entity)
        {
            CurrentSession.Delete(entity);
        }

        [Transaction]
        public UserRoleIdentifier Save(AspnetUsersInRole entity)
        {
            return (UserRoleIdentifier)CurrentSession.Save(entity);
        }

        [Transaction]
        public void Update(AspnetUsersInRole entity)
        {
            CurrentSession.Update(entity);
        }

        [Transaction]
        public void RemoveAppRoleReferences(Guid applicationId, Guid roleId)
        {
            object[] values = new object[] { roleId, applicationId };
            IType[] types = new IType[] { NHibernateUtil.Guid, NHibernateUtil.Guid };
            CurrentSession.Delete("from AspnetUsersInRole as uir where uir.Role.RoleId = ? and uir.Role.ApplicationId = ?", values, types);
        }

        [Transaction(ReadOnly = true)]
        public IList<string> GetRolesNamesForUser(Guid applicationId, Guid userId)
        {
            IList<string> results;

            IQuery query = CurrentSession.CreateQuery("select uir.Role.RoleName from AspnetUsersInRole as uir where uir.User.UserId =: userId and uir.User.ApplicationId = :applicationId");
            query.SetGuid("userId", userId);
            query.SetGuid("applicationId", applicationId);
            results = query.List<string>();
            return results;
        }

        [Transaction(ReadOnly = true)]
        public IList<string> GetUserNamesInRole(Guid applicationId, Guid roleId)
        {
            IList<string> results;

            IQuery query = CurrentSession.CreateQuery("select uir.User.UserName from AspnetUsersInRole as uir where uir.Role.RoleId =: roleId and uir.Role.ApplicationId = :applicationId");
            query.SetGuid("roleId", roleId);
            query.SetGuid("applicationId", applicationId);
            results = query.List<string>();
            return results;
        }

        [Transaction(ReadOnly = true)]
        public int IsUserInrole(Guid applicationId, Guid userId, Guid roleId)
        {
            IList<AspnetUsersInRole> results;

            IQuery query = CurrentSession.CreateQuery("from AspnetUsersInRole as uir where uir.Role.RoleId =: roleId and uir.User.UserId =: userId and uir.Role.ApplicationId = :applicationId and uir.User.ApplicationId = :applicationId");
            query.SetGuid("roleId", roleId);
            query.SetGuid("userId", userId);
            query.SetGuid("applicationId", applicationId);
            results = query.List<AspnetUsersInRole>();
            return results.Count;
        }

        [Transaction]
        public void RemoveUserFromRole(Guid applicationId, Guid userId, Guid roleId)
        {
            object[] values = new object[] { applicationId, roleId, userId  };
            IType[] types = new IType[] { NHibernateUtil.Guid, NHibernateUtil.Guid, NHibernateUtil.Guid };
            CurrentSession.Delete("from AspnetUsersInRole as uir where uir.User.UserId = ? uir.Role.RoleId = ? and uir.Role.ApplicationId = ?", values, types);
        }

        [Transaction(ReadOnly = true)]
        public IList<string> FindUserNamesInRoleByUserLoweredName(Guid applicationId, Guid roleId, string userName)
        {
            IList<string> results;

            IQuery query = CurrentSession.CreateQuery("select uir.User.UserName from AspnetUsersInRole as uir where uir.User.UserName Like :userName and uir.Role.RoleId =: roleId and uir.Role.ApplicationId = :applicationId and uir.User.ApplicationId = :applicationId");
            query.SetGuid("roleId", roleId);
            query.SetString("userName", userName);
            query.SetGuid("applicationId", applicationId);
            results = query.List<string>();
            return results;
        }
    }
}
