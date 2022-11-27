using lesson6.Service;
using Orders.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson6.Controller
{
    internal class OrderController
    {
        private static IProductService _productService;
        private static IOrderService _orderService;
        private static IBuyerService _buyerService;
        public static async Task Menu(IProductService productService, IOrderService orderService, IBuyerService buyerService)
        {
            _productService = productService;
            _orderService = orderService;
            _buyerService = buyerService;

            bool whileOn = true;
            while (whileOn)
            {
                Console.WriteLine("Выберите действие с заказами:");
                Console.WriteLine("1. Отобразить все заказы.");
                Console.WriteLine("2. Добавить заказ.");
                Console.WriteLine("3. Выход.\n");
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                        ShowAllOrders();
                        break;
                    case ConsoleKey.D2:
                        await AddOrder();
                        break;
                    case ConsoleKey.D3:
                        whileOn = false;
                        break;
                    default:
                        break;

                }
            }            
        }

        private static void ShowAllOrders()
        {
            var orders = _orderService.GetAll();
            Console.Clear();
            
            foreach (var order in orders)
            {
                ShowOrder(order);
            }
        }

        private static async Task AddOrder()
        {
            var buyers = _buyerService.GetAll();
            string adress = "street Michurina,10";
            string phone = "+7-901-999-91-91";
            Console.WriteLine("--------------------------------------List buyers-----------------");
            Console.WriteLine("BuyersId    FIO                  Birthday");
            foreach (var buyer in buyers)
            {
                Console.WriteLine($"{buyer.Id}      {buyer.Name} {buyer.LastName} {buyer.Patronymic}    {buyer.Birthday.ToString("dd.MM.yyyy HH:mm:ss")}");
            }
            Console.WriteLine("Please input buyers id");
            int buyerId = Int32.Parse(Console.ReadLine());

            if (buyerId == 0)
                throw new Exception("Buyer not found");

            Console.Clear();
            Console.WriteLine($"Order buyer Id: {buyerId}");

            ShowAllProducts();

            

            List<(int productId, int quantity)> products = new List<(int productId, int quantity)>();


            Console.WriteLine("For exit input '0'.");
            while (true)
            {
                Console.WriteLine("Please input product id");
                int productId = Int32.Parse(Console.ReadLine()); 
                if (productId == 0)
                    break;
                Console.WriteLine("Please input quantity product");
                int quantity = Int32.Parse(Console.ReadLine()); 
                if (quantity == 0)
                    break;
                products.Add(( productId, quantity));

            }

            var order = await _orderService.CreatAsync(buyerId, adress, phone, products);
            
            ShowOrder(order);
        }

        private static void ShowAllProducts()
        {
            var products = _productService.GetAllAsync();
            Console.WriteLine("-------------------List product-------------------");
            Console.WriteLine("{product.Id} {product.Name} {product.Price} {product.Category} ");
            foreach (var product in products)
            {
                Console.WriteLine($"        {product.Id}         {product.Name}     {product.Price}     {product.Category} ");
            }
        }

        private static void ShowOrder(Order order)
        {
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine($"Order Id: {order.Id.ToString()}");
            Console.WriteLine($"Order date: {order.OrderDate.ToString("dd.MM.yyyy HH:mm:ss")}");
            Console.WriteLine($"Order address: {order.Address}");
            Console.WriteLine($"Order buyer: {order.Buyer.Name} {order.Buyer.LastName} {order.Buyer.Patronymic}");
            Console.WriteLine($" Name product   Price product Quantity  Full price");
            foreach (var item in order.Items)
            {
                Console.WriteLine($" {item.Product.Name}   {item.Product.Price} {item.Quantity} {item.Quantity * item.Product.Price}");
            }
            Console.WriteLine("\n");
        }
    }
}
