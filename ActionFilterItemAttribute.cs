using Nop.Plugin.Misc.ActionFilter.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Plugin.Misc.ActionFilter
{

    class ActionFilterItemAttribute : ActionFilterAttribute, IFilterProvider
    {

        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            if (controllerContext.Controller is Nop.Web.Controllers.OrderController && actionDescriptor.ActionName.Equals("GetPdfInvoice", StringComparison.InvariantCultureIgnoreCase))
            //place a breakpoint here so you can see the controller info, remember that this code will catch all
            // actions that fire twice, once before and once after
            // this filter will run the overrides below when the ProductController executes the ProductDetails() action.
            {
                return new List<Filter>() { new Filter(this, FilterScope.Action, 0) };
            }
            return new List<Filter>();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //this is where your custom code goes that will execute before the action executes.
            // put a break point here to see when the productcontroller fires
            var routeValues = new RouteValueDictionary();
            routeValues.Add("selectedIds", filterContext.RouteData.Values["selectedIds"]);
            routeValues.Add("controller", "ActionFilterController");
            routeValues.Add("action", "PdfInvoiceSelected");
            routeValues.Add("area", "Nop.Plugin.Misc.ActionFilter.Controllers");

            filterContext.Result = new RedirectToRouteResult(routeValues);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            //this is where your custom code goes that will execute after the action executes.

        }
    }
}
