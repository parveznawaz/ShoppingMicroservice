using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspnetRunBasics.ApiCollection.Interfaces;
using AspnetRunBasics.ApiCollection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics
{
    public class OrderModel : PageModel
    {
        private readonly IOrderApi _orderApi;

        public OrderModel(IOrderApi orderApi)
        {
            _orderApi = orderApi;
        }


        public IEnumerable<OrderResponseModel> Orders { get; set; } = new List<OrderResponseModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            string userName="parvez";
            Orders = await _orderApi.GetOrdersByUserName(userName);

            return Page();
        }       
    }
}