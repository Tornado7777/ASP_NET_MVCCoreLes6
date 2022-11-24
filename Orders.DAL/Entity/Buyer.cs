using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.DAL.Entity
{
    [Table("Buyers")]
    public class Buyer : NamedEntity
    {
        public string? LastName { get; set; }
        public string? Patronymic { get; set; }
        public DateTime Birthday { get; set; }
    }
}
