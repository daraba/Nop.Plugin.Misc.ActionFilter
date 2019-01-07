using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc.Routes;

namespace Nop.Plugin.Misc.ActionFilter
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {

            routes.MapRoute("Plugin.Misc.ActionFilter.SaveGeneralSettings",
                 "Plugins/ActionFilter/SaveGeneralSettings",
                 new { controller = "ActionFilter", action = "SaveGeneralSettings", },
                 new[] { "Nop.Plugin.Misc.ActionFilter.Controllers" }
            );

            routes.MapLocalizedRoute("Plugin.Misc.ActionFilter.PdfInvoiceSelected",
                     "Plugins/ActionFilter/PdfInvoiceSelected/",
                     new { controller = "ActionFilterController", action = "PdfInvoiceSelected" },
                     new[] { "Nop.Plugin.Misc.ActionFilter.Controllers" });

        }
        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}
