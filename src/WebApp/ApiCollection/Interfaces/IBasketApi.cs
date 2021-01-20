using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetRunBasics.ApiCollection.Models;

namespace AspnetRunBasics.ApiCollection.Interfaces
{
    public interface IBasketApi
    {
        Task<BasketModel> GetBasket(string userName);
        Task<BasketModel> UpdateBasket(BasketModel model);
        Task CheckoutBasket(BasketCheckoutModel model);


    }
}
