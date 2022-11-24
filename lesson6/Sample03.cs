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
using System.Text;
using System.Threading.Tasks;

namespace lesson6
{
    internal class Sample03
    {
        private static IHost? _host;

        //public static IHost Hosting
        //{
        //    get 
        //    { 
        //        return _host ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
        //    }
        //}
        public static IHost Hosting => _host ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        private static IServiceProvider Services => Hosting.Services;
        static async Task Main(string[] args)
        {
            await Hosting.StartAsync();
            await PrintBuyersAsync();
            Console.ReadKey();
            await Hosting.StopAsync();
        }

        private static async Task PrintBuyersAsync()
        {
            await using var serviceScope = Services.CreateAsyncScope();
            var services = serviceScope.ServiceProvider;

            var context = services.GetRequiredService<OrdersDbContext>();
            var logger = services.GetRequiredService<ILogger<Sample03>>();

            foreach (var buyer in context.Buyers)
            {
                logger.LogInformation($"Покупапатель >>> {buyer.LastName} {buyer.Name} {buyer.Patronymic} {buyer.Birthday.ToShortDateString()}");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
           return Host
                .CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(options =>
                options.AddJsonFile("appsetting.json"))
                .ConfigureAppConfiguration(options =>
                options
                    .AddJsonFile("appsetting.json")
                    .AddXmlFile("appsetting.xml", true)
                    .AddIniFile("appsetting.ini", true)
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
