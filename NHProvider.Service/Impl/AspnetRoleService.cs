using System.Collections.Generic;
using NHibernate.Type;
using Spring.Transaction.Interceptor;
using nhprovider.service.contracts;
using nhprovider.data.contracts;
using nhprovider.model;
using System;

namespace nhprovider.service.impl
{
    public class AspnetRoleService : IAspnetRoleService
    {
        private IAspnetRoleDao _aspnetRoleDao;

        public IAspnetRoleDao AspnetRoleDao
        {
            set { _aspnetRoleDao = value; }
        }

        public int SaveAspnetRole(AspnetRole aspRole)
        {
            _aspnetRoleDao.Save(aspRole);
            if (aspRole != null)
                return 1;
            return 0;
        }

        public int UpdateAspnetRole(AspnetRole aspRole)
        {
            _aspnetRoleDao.Update(aspRole);
            if (aspRole != null)
                return 1;
            return 0;
        }

        public void DeleteRole(AspnetRole aspRole)
        {
            _aspnetRoleDao.Delete(aspRole);
        }

        public AspnetRole GetAspnetRole(string roleName)
        {
            return _aspnetRoleDao.GetAspnetRole(roleName);
        }

        public IList<string> GetRoleNames(AspnetApplication application)
        {
            return _aspnetRoleDao.GetRoleNames(application);
        }

        public IList<AspnetRole> GetAspnetRoles(string roleName, Guid applicationId)
        {
            return _aspnetRoleDao.GetAspnetRoles(roleName, applicationId);
        }

    }
}
