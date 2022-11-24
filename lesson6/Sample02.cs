using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Orders.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson6
{
    internal class Sample02
    {
        static void Main(string[] args)
        {
            var serviceBuilder = new ServiceCollection();

            #region Configure EF DBContext Service

            serviceBuilder.AddDbContext<OrdersDbContext>(options =>
            {
                options
                .UseSqlServer("data source=localhost;initial catalog=OrdersDB;User Id=OrdersDbUser;Password=12345;App=EntityFramework; trustServerCertificate=true");
            });

            #endregion

            //serviceBuilder.AddSingleton<IService, ServiceImplementation>();

            var serviceProvide = serviceBuilder.BuildServiceProvider();

            var context = serviceProvide.GetRequiredService<OrdersDbContext>();

            foreach (var buyer in context.Buyers)
            {
                Console.WriteLine($"{buyer.LastName} {buyer.Name} {buyer.Patronymic} {buyer.Birthday.ToShortDateString()}");
            }

        }
    }
}
