using EPiServer.Web.Mvc.Html;
using StructureMap;
using WinstonTraining.Core.Services;
using WinstonTraining.Core.Services.Interfaces;
using WinstonTraining.Web.CustomCustomArea;

namespace WinstonTraining.Web.Infrastructure.IoC
{
    public class SiteRegistry : Registry
    {
        public SiteRegistry()
        {
            //rs: example of defining an IoC rule
            For<ITestService>().Use<AltTestService>();

            //rs: example of defining an IoC rule for a singleton service
            //For<IMyCustomerSingletonService>().Singleton().User<MyCustomSingletonService>();

            //rs: example of defining an IoC rule that overrides a built in Episerver service
            For<ContentAreaRenderer>().Use<CustomContentAreaRenderer>();
        }
    }
}