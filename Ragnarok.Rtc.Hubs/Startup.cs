using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Ninject;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(Ragnarok.Rtc.Hubs.Startup))]

namespace Ragnarok.Rtc.Hubs
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR(
                new HubConfiguration
                {
                    Resolver = new NinjectSignalRDependencyResolver(CreateContainer()),
                    EnableDetailedErrors = true
                });

        }

        private IKernel CreateContainer()
        {
            var kernel =  new StandardKernel();

            kernel.Bind<Data.IPurchaseOrderRepository, Data.IPurchaseOrderView>()
                .ToMethod(ctx => new Data.PurchaseOrderRepository("database"));
            
            return kernel;
        }
    }

    internal class NinjectSignalRDependencyResolver : DefaultDependencyResolver
    {
        private readonly IKernel _kernel;
        public NinjectSignalRDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

        public override object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType) ?? base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType).Concat(base.GetServices(serviceType));
        }
    }
}
