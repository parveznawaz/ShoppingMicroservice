using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspnetRunBasics.ApiCollection;
using AspnetRunBasics.ApiCollection.Interfaces;
using AspnetRunBasics.ApiCollection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICatalogApi _catalogApi;
        private readonly IBasketApi _basketApi;

        public IndexModel(ICatalogApi catalogApi, IBasketApi basketApi)
        {
            _catalogApi = catalogApi ?? throw new ArgumentNullException(nameof(catalogApi)); ;
            _basketApi = basketApi?? throw new ArgumentNullException(nameof(_basketApi));
        }

        public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            ProductList = await _catalogApi.GetCatalog();
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(string productId)
        {
            var product = await _catalogApi.GetCatalog(productId);
            var userName = "parvez";
            var basket = await _basketApi.GetBasket(userName);
            basket.Items.Add(new BasketItemModel()
            {
                ProductId = productId,
                ProductName = product.Name,
                Price = product.Price,
                Quality = 1
            });

            var basketUpdated = await _basketApi.UpdateBasket(basket);
            return RedirectToPage("Cart");
        }
    }
}
