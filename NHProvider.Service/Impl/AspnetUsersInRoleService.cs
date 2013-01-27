using System.Collections.Generic;
using NHibernate.Type;
using Spring.Transaction.Interceptor;
using nhprovider.service.contracts;
using nhprovider.data.contracts;
using nhprovider.model;
using System;

namespace nhprovider.service.impl
{
    public class AspnetUsersInRoleService : IAspnetUsersInRoleService
    {
        private IAspnetUsersInRoleDao _aspnetUsersInRoleDao;

        public IAspnetUsersInRoleDao AspnetUsersInRoleDao
        {
            set { _aspnetUsersInRoleDao = value; }
        }

        public int SaveAspnetUsersInRole(AspnetUsersInRole aspUsersInRole)
        {
            _aspnetUsersInRoleDao.Save(aspUsersInRole);
            if (aspUsersInRole != null)
                return 1;
            return 0;
        }

        public void DeleteAspnetUsersInRole(AspnetUsersInRole aspUsersInRole)
        {
            _aspnetUsersInRoleDao.Delete(aspUsersInRole);
        }

        public AspnetUsersInRole GetAspnetUsersInRole(AspnetRole role, AspnetUser user)
        {
            UserRoleIdentifier id = new UserRoleIdentifier();
            id.RoleId = role.RoleId;
            id.UserId = user.UserId;
            return _aspnetUsersInRoleDao.GetById(id);
        }

        public int UpdateAspnetUser(AspnetUsersInRole usersInRole)
        {
            _aspnetUsersInRoleDao.Update(usersInRole);
            if (usersInRole != null)
                return 1;
            return 0;
        }


        public void RemoveAppRoleReferences(Guid applicationId, Guid roleId)
        {
            _aspnetUsersInRoleDao.RemoveAppRoleReferences(applicationId, roleId);
        }

        public IList<string> GetRolesNamesForUser(Guid applicationId, Guid userId)
        {
            return _aspnetUsersInRoleDao.GetRolesNamesForUser(applicationId, userId);
        }

        public IList<string> GetUserNamesInRole(Guid applicationId, Guid roleId)
        {
            return _aspnetUsersInRoleDao.GetUserNamesInRole(applicationId, roleId);
        }

        public int IsUserInrole(Guid applicationId, Guid userId, Guid roleId)
        {
            return _aspnetUsersInRoleDao.IsUserInrole(applicationId, userId, roleId);
        }

        public void RemoveUserFromRole(Guid applicationId, Guid userId, Guid roleId)
        {
            _aspnetUsersInRoleDao.RemoveUserFromRole(applicationId, userId, roleId);
        }

        public IList<string> FindUserNamesInRoleByUserLoweredName(Guid applicationId, Guid roleId, string userName)
        {
            return _aspnetUsersInRoleDao.FindUserNamesInRoleByUserLoweredName(applicationId, roleId, userName);
        }
    }
}
