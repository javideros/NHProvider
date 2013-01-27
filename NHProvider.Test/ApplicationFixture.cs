using System;
using NUnit.Framework;
using nhprovider.service.contracts;
using Spring.Context;
using Spring.Context.Support;
using nhprovider.model;

namespace NHProvider.Test
{
    [TestFixture]
    public class ApplicationFixture
    {
        private IAspnetApplicationService appService;

        public IAspnetApplicationService ApplicationService
        {
            set
            {
                appService = value;
            }
        }

        [Test]
        public void CreateApplication() {
            if (appService == null)
            {
                IApplicationContext context = ContextRegistry.GetContext();
                appService = (IAspnetApplicationService)context.GetObject("AspnetApplicationService");
            }

            Assert.IsNotNull(appService);

            AspnetApplication app = appService.CreateOrLoadApplication("TestApp");
            Assert.AreNotEqual(-1, app.ApplicationId);

            Console.WriteLine("The id of TestApp is :" + app.ApplicationId);

            app = appService.GetApplication(app.ApplicationId);
            appService.DeleteApplication(app);
            
        }
    }
}