using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using lesson6.Autofac;
using lesson6.Controller;
using lesson6.Service;
using lesson6.Service.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orders.DAL;
using Orders.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace lesson6
{
    internal class hw6
    {
        private static IHost? _host;

        public static IHost Hosting => _host ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        private static IServiceProvider Services => Hosting.Services;
        static async Task Main(string[] args)
        {
            await Hosting.StartAsync();
            await PrintBuyersAsync();
            await MenuProduct();
            //Console.ReadKey();
            await Hosting.StopAsync();
        }

        private static async Task MenuProduct()
        {
            await using var serviceScope = Services.CreateAsyncScope();
            var services = serviceScope.ServiceProvider;

            var context = services.GetRequiredService<OrdersDbContext>();
            var logger = services.GetRequiredService<ILogger<ProductController>>();
            var productService = services.GetRequiredService<ProductService>();

            ProductController.Menu(productService);
        }

        private static async Task PrintBuyersAsync()
        {
            var rnd = new Random();

            await using var serviceScope = Services.CreateAsyncScope();
            var services = serviceScope.ServiceProvider;

            var context = services.GetRequiredService<OrdersDbContext>();
            var logger = services.GetRequiredService<ILogger<hw6>>();

            foreach (var buyer in context.Buyers)
            {
                logger.LogInformation($"Покупапатель >>> {buyer.LastName} {buyer.Name} {buyer.Patronymic} {buyer.Birthday.ToShortDateString()}");
            }
            var orderService = services.GetRequiredService<IOrderService>();

            Console.WriteLine("PrintBuyersAsync");
            //await orderService.CreatAsunc(
            //    rnd.Next(1, 5),
            //    "123,Russian, Address",
            //    "+7(903)-000-00-01",
            //    new (int, int)[]
            //    {
            //        new ValueTuple<int,int>(1, 1)
            //    });
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host
                 .CreateDefaultBuilder(args)
                 .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                 .ConfigureContainer<ContainerBuilder>(container => //autofac
                 {

                     container.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
                     container.RegisterType<ProductService>().InstancePerLifetimeScope();


                 })
                 .ConfigureHostConfiguration(options =>
                 options.AddJsonFile("appsetting.json"))
                 .ConfigureAppConfiguration(options =>
                 options
                     .AddJsonFile("appsetting.json")
                     
                     .AddEnvironmentVariables()
                     .AddCommandLine(args))
                 .ConfigureLogging(options =>
                 options
                     .ClearProviders() //using Microsoft.Extensions.Logging;
                     .AddConsole()
                     .AddDebug())
                 .ConfigureServices(ConfigureServices);
        }

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddDbContext<OrdersDbContext>(options =>
            {
                options
                .UseSqlServer(host.Configuration["Settings:DatabseOptions:ConnectionsString"]);
            });

        }
    }
}
