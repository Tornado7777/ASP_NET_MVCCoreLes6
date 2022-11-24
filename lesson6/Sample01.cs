using Microsoft.EntityFrameworkCore;
using Orders.DAL;
using Orders.DAL.Entity;

namespace lesson6
{
    internal class Sample01
    {
        static void Main(string[] args)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<OrdersDbContext>()
                .UseSqlServer("data source=localhost;initial catalog=OrdersDB;User Id=OrdersDbUser;Password=12345;App=EntityFramework; trustServerCertificate=true");

            using (var context = new OrdersDbContext(dbContextOptionsBuilder.Options))
            {
                context.Database.EnsureCreated();

                //https://randomus.ru/name

                if (!context.Buyers.Any())
                {
                    context.Buyers.Add(new Buyer
                    {
                        LastName = "Трофимов",
                        Name = "Алексей",
                        Patronymic = "Артемьевич",
                        Birthday = DateTime.Now.AddYears(-28).Date,
                    });
                    context.Buyers.Add(new Buyer
                    {
                        LastName = "Зеленин",
                        Name = "Николай",
                        Patronymic = "Данилович",
                        Birthday = DateTime.Now.AddYears(-38).Date,
                    });
                    context.Buyers.Add(new Buyer
                    {
                        LastName = "Ермаков",
                        Name = "Христафор",
                        Patronymic = "Банифатич",
                        Birthday = DateTime.Now.AddYears(-25).Date,
                    });
                    context.Buyers.Add(new Buyer
                    {
                        LastName = "Федоров",
                        Name = "Илья",
                        Patronymic = "Иванович",
                        Birthday = DateTime.Now.AddYears(-19).Date,
                    });
                    context.Buyers.Add(new Buyer
                    {
                        LastName = "Леонов",
                        Name = "Тигран",
                        Patronymic = "Андреевич",
                        Birthday = DateTime.Now.AddYears(-23).Date,
                    });

                    context.SaveChanges();
                }

                foreach (var buyer in context.Buyers)
                {
                    Console.WriteLine($"{buyer.LastName} {buyer.Name} {buyer.Patronymic} { buyer.Birthday.ToShortDateString()}");
                }
            }

        }
    }
}