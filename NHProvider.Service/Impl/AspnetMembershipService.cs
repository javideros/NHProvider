using System.Collections.Generic;
using NHibernate.Type;
using Spring.Transaction.Interceptor;
using nhprovider.service.contracts;
using nhprovider.data.contracts;
using nhprovider.model;
using System;

namespace nhprovider.service.impl
{
    public class AspnetMembershipService : IAspnetMembershipService
    {
        private IAspnetMembershipDao _aspnetMembershipDao;

        public IAspnetMembershipDao AspnetMembershipDao
        {
            set { _aspnetMembershipDao = value; }
        }

        public int UpdateMembership(AspnetMembership membership)
        {
            _aspnetMembershipDao.Update(membership);
            if (membership != null)
                return 1;
            return 0;
        }

        public AspnetMembership GetMembership(Guid guid, AspnetApplication application)
        {
            return _aspnetMembershipDao.GetMembership(guid, application);
        }

        public AspnetMembership GetMembership(Guid guid)
        {
            return _aspnetMembershipDao.GetById(guid);
        }

        public AspnetMembership GetMembership(string userName)
        {
            return _aspnetMembershipDao.GetMembership(userName);
        }

        public int SaveMembership(AspnetMembership user)
        {
            _aspnetMembershipDao.Save(user);
            if (user != null)
                return 1;
            return 0;
        }

        public IList<AspnetMembership> GetMembershipByAppIdAndUserName(AspnetApplication application, string username)
        {
           return _aspnetMembershipDao.GetMembershipByAppIdAndUserName(application, username);
        }

        public IList<AspnetMembership> GetMembershipsByAppIdAndUserEmail(AspnetApplication application, string email)
        {
            return _aspnetMembershipDao.GetMembershipsByAppIdAndUserEmail(application, email);
        }

        public AspnetMembership GetAspnetMembership(string username, AspnetApplication application)
        {
            return _aspnetMembershipDao.GetAspnetMembership(username, application);
        }

        public IList<AspnetMembership> GetAllApplicationUsers(string applicationName, int pageIndex, int pageSize, out int totalRecords)
        {
            return _aspnetMembershipDao.GetAllApplicationUsers(applicationName, pageIndex, pageSize, out totalRecords);
        }

    }
}
