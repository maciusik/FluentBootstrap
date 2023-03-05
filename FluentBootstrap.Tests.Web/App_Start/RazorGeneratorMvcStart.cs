using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using RazorGenerator.Mvc;
using FluentBootstrap.Tests.Web;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(FluentBootstrap.Tests.Web.App_Start.RazorGeneratorMvcStart), "Start")]

namespace FluentBootstrap.Tests.Web.App_Start {
    public static class RazorGeneratorMvcStart {
        public static void Start() {
            var engine = new PrecompiledMvcEngine(typeof(RazorGeneratorMvcStart).Assembly) {
                UsePhysicalViewsIfNewer = HttpContextHelper.Current.Request.IsLocal
            };

            ViewEngines.Engines.Insert(0, engine);

            // StartPage lookups are done by WebPages. 
            VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);
        }
    }
}
