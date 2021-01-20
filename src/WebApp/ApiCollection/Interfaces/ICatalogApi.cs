using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetRunBasics.ApiCollection.Models;

namespace AspnetRunBasics.ApiCollection.Interfaces
{
    public interface ICatalogApi
    {
        Task<IEnumerable<CatalogModel>> GetCatalog();
        Task<IEnumerable<CatalogModel>> GetCatalogByCatagory(string catagory);
        Task<CatalogModel> GetCatalog(string id);
        Task<CatalogModel> CreateCatalog(CatalogModel model);

    }
}
