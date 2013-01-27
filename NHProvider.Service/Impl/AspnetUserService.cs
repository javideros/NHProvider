using System.Collections.Generic;
using NHibernate.Type;
using Spring.Transaction.Interceptor;
using nhprovider.service.contracts;
using nhprovider.data.contracts;
using nhprovider.model;
using System;

namespace nhprovider.service.impl
{
    public class AspnetUserService : IAspnetUserService
    {
        private IAspnetUserDao _aspnetUserDao;

        public IAspnetUserDao AspnetUserDao
        {
            set { _aspnetUserDao = value; }
        }

        public int SaveAspnetUser(AspnetUser aspUser)
        {
            _aspnetUserDao.Save(aspUser);
            if (aspUser != null)
                return 1;
            return 0;
        }

        public void RemoveAppUserReferences(AspnetApplication application, string username)
        {
            _aspnetUserDao.RemoveAppUserReferences(application, username);
        }

        public int DeleteUser(string username)
        {
            return _aspnetUserDao.DeletebyName(username);
        }

        public AspnetUser GetAspnetUser(string username)
        {
            return _aspnetUserDao.GetAspnetUser(username);
        }

        public int UpdateAspnetUser(AspnetUser aspUser)
        {
            _aspnetUserDao.Update(aspUser);
            if (aspUser != null)
                return 1;
            return 0;
        }

        public IList<AspnetUser> GetUsersById(System.Guid guid)
        {
            return _aspnetUserDao.GetUsersById(guid);
        }

        public IList<AspnetUser> GetUsersByName(string username)
        {
            return _aspnetUserDao.GetUsersByName(username);
        }

        public int GetUsersOnline(AspnetApplication application, System.DateTime compareTime)
        {
            return _aspnetUserDao.GetUsersOnline(application, compareTime);
        }

        public AspnetUser GetAspnetUser(string username, AspnetApplication application)
        {
            return _aspnetUserDao.GetAspnetUser(username, application);
        }

        public IList<AspnetUser> GetUsersByEmail(string email, string applicationName)
        {
            return _aspnetUserDao.GetUsersByEmail(email, applicationName);
        }

    }
}
