using Nop.Core.Plugins;
using Nop.Plugin.Misc.ActionFilter.Controllers;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Plugin.Misc.ActionFilter
{
    class ActionFilter : BasePlugin, IMiscPlugin
    {
        private readonly ISettingService _settingService;
        private readonly ActionFilterSettings _actionFilterSettings;


        #region Ctor
        public ActionFilter(
            ActionFilterSettings actionFilterSettings,
            ISettingService settingService)
        {
            this._actionFilterSettings = actionFilterSettings;
            this._settingService = settingService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets a route for provider configuration
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "ActionFilter";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.Misc.ActionFilter.Controllers" }, { "area", null } };
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            //settings
            var actionFilterSettings = new ActionFilterSettings
            {
            };
            _settingService.SaveSetting(actionFilterSettings);


            //database objects
            //_objectContext.Install();

            //locales
            this.AddOrUpdatePluginLocaleResource("Plugins.Misc.ActionFilter.Fields.beallitasok", "ActionFilter beállítások");

            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<ActionFilterSettings>();

            //database objects
            //_objectContext.Uninstall();

            //locales
            this.DeletePluginLocaleResource("Plugins.Misc.ActionFilter.Fields.beallitasok");

            base.Uninstall();
        }
        #endregion

    }


}

