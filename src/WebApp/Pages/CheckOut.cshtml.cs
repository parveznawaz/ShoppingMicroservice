﻿using System;
using System.Threading.Tasks;
using AspnetRunBasics.ApiCollection.Interfaces;
using AspnetRunBasics.ApiCollection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics
{
    public class CheckOutModel : PageModel
    {
        private readonly ICatalogApi _catalogApi;
        private readonly IBasketApi _basketApi;

        public CheckOutModel(ICatalogApi catalogApi, IBasketApi basketApi)
        {
            _catalogApi = catalogApi;
            _basketApi = basketApi;
        }


        [BindProperty]
        public BasketCheckoutModel Order { get; set; }

        public BasketModel Cart { get; set; } = new BasketModel();

        public async Task<IActionResult> OnGetAsync()
        {
            var userName = "parvez";
            Cart = await _basketApi.GetBasket(userName);
            return Page();
        }

        public async Task<IActionResult> OnPostCheckOutAsync()
        {
            var userName = "parvez";
            Cart = await _basketApi.GetBasket(userName);
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Order.UserName = userName;
            Order.TotalPrice = Cart.TotalPrice;

            await _basketApi.CheckoutBasket(Order);

            return RedirectToPage("Confirmation", "OrderSubmitted");
        }       
    }
}