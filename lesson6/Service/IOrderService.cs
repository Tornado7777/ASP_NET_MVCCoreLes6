using Orders.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson6.Service
{
    internal interface IOrderService
    {
        Task<Order> CreatAsunc(
            int buyerId, 
            string address, 
            string phone, 
            IEnumerable<(int productId, int quantity)> products);
    }
}
