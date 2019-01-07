using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.ActionFilter
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<ActionFilterItemAttribute>().As<System.Web.Mvc.IFilterProvider>();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}