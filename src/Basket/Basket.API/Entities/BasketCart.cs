using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Entities
{
    public class BasketCart
    {
        public string UserName { get; set; }
        public List<BasektCartItem> Items { get; set; } = new List<BasektCartItem>();

        public BasketCart()
        {
            
        }

        public BasketCart(string userName)
        {
            UserName = userName;
        }

        public decimal TotalPrice { get
            {
                return Items.Sum(item => item.Price * item.Quality);
            }
        }
    }
}
