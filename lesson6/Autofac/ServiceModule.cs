using Autofac;
using lesson6.Service.Impl;
using lesson6.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson6.Autofac
{
    internal class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<OrderService>()
                .As<IOrderService>()
                .InstancePerLifetimeScope();
        }
    }
}
