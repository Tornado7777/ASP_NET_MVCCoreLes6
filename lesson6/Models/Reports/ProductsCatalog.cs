using Orders.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson6.Models.Reports
{
    internal class ProductsCatalog
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
