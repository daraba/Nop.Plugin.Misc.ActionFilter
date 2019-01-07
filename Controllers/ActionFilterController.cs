using Nop.Core;
using Nop.Services.Security;
using Nop.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Core.Domain.Orders;
using Nop.Services.Orders;
using Nop.Services.Localization;
using Nop.Plugin.Misc.ActionFilter.Services;
using System.IO;

namespace Nop.Plugin.Misc.ActionFilter.Controllers
{
    public class ActionFilterController : BaseAdminController
    {
        private readonly IOrderService _orderService;
        private readonly IWorkContext _workContext;
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;

        public ActionFilterController(IOrderService orderService,
            IWorkContext workContext,
            ILocalizationService localizationService,
            IPermissionService permissionService)
        {
            this._orderService = orderService;
            this._workContext = workContext;
            this._localizationService = localizationService;
            this._permissionService = permissionService;
        }


        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            //little hack here
            //always set culture to 'en-US' (Telerik has a bug related to editing decimal values in other cultures). Like currently it's done for admin area in Global.asax.cs
            CommonHelper.SetTelerikCulture();

            base.Initialize(requestContext);
        }

        [NonAction]
        protected virtual bool HasAccessToOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            if (_workContext.CurrentVendor == null)
                //not a vendor; has access
                return true;

            var vendorId = _workContext.CurrentVendor.Id;
            var hasVendorProducts = order.OrderItems.Any(orderItem => orderItem.Product.VendorId == vendorId);
            return hasVendorProducts;
        }


        [ChildActionOnly]
        public ActionResult Configure()
        {
            var model = new Models.ActionFilterSettings();
            //other settings

            return View("~/Plugins/Misc.ActionFilter/Views/StockTake/Configure.cshtml", model);
        }


        [HttpPost]
        public ActionResult PdfInvoiceSelected(string selectedIds)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return AccessDeniedView();

            var orders = new List<Order>();
            if (selectedIds != null)
            {
                var ids = selectedIds
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
                orders.AddRange(_orderService.GetOrdersByIds(ids));
            }

            //a vendor should have access only to his products
            if (_workContext.CurrentVendor != null)
            {
                orders = orders.Where(HasAccessToOrder).ToList();
            }

            //ensure that we at least one order selected
            if (!orders.Any())
            {
                ErrorNotification(_localizationService.GetResource("Admin.Orders.PdfInvoice.NoOrders"));
                return RedirectToAction("List");
            }

            byte[] bytes;
            foreach(Order order in orders) { 
                using (var stream = new MemoryStream())
                {
                    //change to service
                    var g = new GetSzamla();
                    g.PrintOrderToPdf(stream, order );
                    bytes = stream.ToArray();
                }
                return File(bytes, MimeTypes.ApplicationPdf, "orders.pdf");
            }
            return RedirectToAction("List");
        }



    }
}