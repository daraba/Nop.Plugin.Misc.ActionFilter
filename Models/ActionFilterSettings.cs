using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Misc.ActionFilter.Models
{
    public class ActionFilterSettings : BaseNopModel
    {
        [NopResourceDisplayName("Plugins.Misc.ActionFilter.Fields.felhasznaloField")]
        public string FelhasznaloField { get; set; }
        [NopResourceDisplayName("Plugins.Misc.ActionFilter.Fields.jelszoField")]
        public string JelszoField { get; set; }
    }
}