using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using System.Web.Http;
using System.Web.Mvc;

namespace WinstonTraining.Web.Infrastructure.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class SiteInitialization : IConfigurableModule
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

        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            var container = context.StructureMap();

            //rs: add the structure map registry
            container.Configure(cfg => cfg.AddRegistry<SiteRegistry>());

            // Wire up MVC dependency resolvers (constructor based injection)
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
        }
    }
}