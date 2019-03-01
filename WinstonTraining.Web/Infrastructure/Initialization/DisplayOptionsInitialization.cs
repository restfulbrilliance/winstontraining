using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using WinstonTraining.Web.Constants;

namespace WinstonTraining.Web.Infrastructure.Initialization
{
    /// <summary>
    /// This initialization module defines custom display options tags that appear in
    /// the CMS editor interface per block within a content area.  It does not affect
    /// the rendering of the blocks, it simply adds the display options choices.
    /// </summary>
    [InitializableModule]
    [ModuleDependency(typeof(SiteInitialization))]
    public class DisplayOptionsInitialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            if (context.HostType == HostType.WebApplication)
            {
                var options = ServiceLocator.Current.GetInstance<DisplayOptions>();
                options.Add(DisplayOptionsTags.Full, DisplayOptionsTags.Full, DisplayOptionsTags.Full, "Full width", "epi-icon__layout--full");
                options.Add(DisplayOptionsTags.Half, DisplayOptionsTags.Half, DisplayOptionsTags.Half, "Half width", "epi-icon__layout--half");
            }
        }

        public void Uninitialize(InitializationEngine context)
        {
            //Add uninitialization logic
        }
    }
}