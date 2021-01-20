using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetRunBasics.ApiCollection.Models;

namespace AspnetRunBasics.ApiCollection.Interfaces
{
    public interface IOrderApi
    {
        Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);

    }
}
