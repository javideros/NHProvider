using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Type;
using Spring.Transaction.Interceptor;
using nhprovider.data.contracts;
using nhprovider.model;
using nhprovider.service.contracts;

namespace nhprovider.service.impl
{
    public class AspnetApplicationService : IAspnetApplicationService
    {
        private IAspnetApplicationDao _aspnetApplicationDao;

        public IAspnetApplicationDao AspnetApplicationDao
        {
            set { _aspnetApplicationDao = value; }
        }

        public AspnetApplication CreateOrLoadApplication(string applicationName)
        {
            return _aspnetApplicationDao.CreateOrLoadApplication(applicationName);
        }


        public void DeleteApplication(AspnetApplication app)
        {
            _aspnetApplicationDao.Delete(app);
        }

        public AspnetApplication GetApplication(Guid guid)
        {
            return _aspnetApplicationDao.GetById(guid);
        }

        public void UpdateApplication(AspnetApplication application)
        {
            _aspnetApplicationDao.Update(application);
        }
    }
}
