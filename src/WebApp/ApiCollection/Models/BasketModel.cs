using System.Collections.Generic;

namespace AspnetRunBasics.ApiCollection.Models
{
    public class BasketModel
    {
        public string UserName { get; set; }
        public List<BasketItemModel> Items { get; set; } = new List<BasketItemModel>();
        public decimal TotalPrice { get; set; }
    }
}