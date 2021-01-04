using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetPreconfiguredProducts());
            }
            
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Name = "IPhone 12",
                    Summery = "This is phone is great",
                    Description = "IPhone is expensive",
                    ImageFile = "product.1.jpg",
                    Price = 1231.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Name = "Samsung Notepad 20",
                    Summery = "This is phone is great",
                    Description = "Nice camera",
                    ImageFile = "product.2.jpg",
                    Price = 1500.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Name = "IPhone 11",
                    Summery = "This is phone is great",
                    Description = "IPhone is expensive",
                    ImageFile = "product.1.jpg",
                    Price = 700.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Name = "Galaxy 10",
                    Summery = "This is phone is great",
                    Description = "nice game",
                    ImageFile = "product.4.jpg",
                    Price = 1131.00M,
                    Category = "Smart Phone"
                }
                
            };
        }
    }
}