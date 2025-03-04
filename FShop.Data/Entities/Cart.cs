using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FShop.Data.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<ProductCart> ProductCarts { get; set; }
    }
}
