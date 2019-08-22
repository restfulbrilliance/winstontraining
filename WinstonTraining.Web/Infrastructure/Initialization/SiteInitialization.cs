using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using System.Web.Http;
using System.Web.Mvc;
using WinstonTraining.Web.Infrastructure.IoC;

namespace WinstonTraining.Web.Infrastructure.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class SiteInitialization : IConfigurableModule
    {
        public void Initialize(InitializationEngine context)
        {
           //Add initialization logic 
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
            DependencyResolver.SetResolver(new StructureMapMvcDependencyResolver(container));

            GlobalConfiguration.Configure(config =>
            {
                // Web API configuration takes place in a seperate class
                WebApiConfig.Register(config);

                // Wire up Web API dependency resolvers (constructure based injection)
                config.DependencyResolver = new StructureMapWebApiDependencyResolver(context.StructureMap());
            });
        }
    }
}