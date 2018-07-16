using System;
using System.Linq;
using System.Web.Http;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;

namespace WinstonTraining.Web.Infrastructure
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class SiteInitialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            GlobalConfiguration.Configure(config =>
            {
                WebApiConfig.Register(config);
            });
            //Add initialization logic, this method is called once after CMS has been initialized
        }

        public void Uninitialize(InitializationEngine context)
        {
            //Add uninitialization logic
        }
    }
}