using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetRunBasics.ApiCollection.Interfaces;
using AspnetRunBasics.ApiCollection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics
{
    public class ProductModel : PageModel
    {
        private readonly ICatalogApi _catalogApi;
        private readonly IBasketApi _basketApi;

        public ProductModel(ICatalogApi catalogApi, IBasketApi basketApi)
        {
            _catalogApi = catalogApi;
            _basketApi = basketApi;
        }

        public IEnumerable<string> CategoryList { get; set; } = new List<string>();
        public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();


        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(string categoryName)
        {
            var productList = await _catalogApi.GetCatalog();
            CategoryList = productList.Select(x => x.Category).Distinct();
            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                ProductList = productList.Where(x => x.Category == categoryName);
                SelectedCategory = categoryName;
            }
            else
            {
                ProductList = productList;
            }

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