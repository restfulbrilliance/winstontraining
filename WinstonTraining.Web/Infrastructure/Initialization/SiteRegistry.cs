using EPiServer.Web.Mvc.Html;
using StructureMap;
using WinstonTraining.Web.CustomCustomArea;

namespace WinstonTraining.Web.Infrastructure.Initialization
{
    public class SiteRegistry : Registry
    {
        public SiteRegistry()
        {
            //rs: example of defining an IoC rule
            //For<IMyCustomerService>().Use<MyCustomService>();

            //rs: example of defining an IoC rule for a singleton service
            //For<IMyCustomerSingletonService>().Singleton().User<MyCustomSingletonService>();

            //rs: example of defining an IoC rule that overrides a built in Episerver service
            For<ContentAreaRenderer>().Use<CustomContentAreaRenderer>();
        }
    }
}