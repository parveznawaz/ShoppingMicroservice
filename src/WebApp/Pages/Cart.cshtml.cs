using System;
using System.Linq;
using System.Threading.Tasks;
using AspnetRunBasics.ApiCollection.Interfaces;
using AspnetRunBasics.ApiCollection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics
{
    public class CartModel : PageModel
    {
        private readonly IBasketApi _basketApi;

        public CartModel(IBasketApi basketApi)
        {
            _basketApi = basketApi;
        }


        public BasketModel Cart { get; set; } = new BasketModel();        

        public async Task<IActionResult> OnGetAsync()
        {
            string userName="parvez";
            Cart = await _basketApi.GetBasket(userName);

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(string productId)
        {
            string userName="parvez";
            var basket = await _basketApi.GetBasket(userName);
            var item = basket.Items.Single(x => x.ProductId == productId);
            var basketUpdated = await _basketApi.UpdateBasket(basket);

            return RedirectToPage();
        }
    }
}